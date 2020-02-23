using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // set up bullet attributes
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public int damage = 50;
    public GameObject impactEffect;

    // set the bullet's target
    public void Seek(Transform _target)
    {
        target = _target;
    }

  
    // Update is called once per frame
    void Update()
    {

        // if target dies while bullet is travelling, destroy bullet
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // bullet has hit the target
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
        
    }

    // the bullet has hit the target
    void HitTarget()
    {

        // spawn in the bullet particle effect
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f); // destroy particle effect after 5 seconds
        
        // if bullet, damage the target; if carrot/missile, explode the target
        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        
        // destroy the bullet
        Destroy(gameObject);

    }

    // the carrot is exploding on the target
    void Explode()
    {

        // for each enemy in impact radius, damage them
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    // an enemy has been damaged by bullet or carrot
    void Damage (Transform enemy)
    {

        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }

    }

    // this draws lines around radius of explosion
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
