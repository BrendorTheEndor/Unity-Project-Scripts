using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    // set up turret attributes

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;
    public float turnSpeed = 10f;

    [Header("Use Bullets (default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f; // fire 1 bullet per second
    

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    public Light laserLight;
    public int damageOverTime = 30;
    public float slowPercent = 0.3f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float fireCountdown = 0f;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        float repeatRate = 0.5f;
        InvokeRepeating("UpdateTarget", 0f, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

        // if there is no target, make sure laser beamer is not shooting
        if(target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    laserLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        // call shoot for turret/launcher or laser for laser beamer
        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    // splase the enemy with a laser
    void Laser()
    {

        // cause the enemy to take damage
        if(targetEnemy.health > 0)
        {
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        }
        
        // slow the enemy down
        targetEnemy.Slow(slowPercent);

        // show laser effects
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
            laserLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        laserEffect.transform.position = target.position + dir.normalized;
        laserEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    // lock onto a target
    void LockOnTarget()
    {

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    // for carrot launcher and standard turret, shoot the enemy
    void Shoot()
    {

        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }

    }

    // change the turret's target to nearest enemy
    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // find enemy nearest to turret
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // change the turret's target if nearest enemy is within range
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    // draw a sphere around turret's range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
