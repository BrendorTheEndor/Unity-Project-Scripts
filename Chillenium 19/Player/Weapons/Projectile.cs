using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    enum BulletType { RailgunShot, SpreadShot };

    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] int damagePerBullet = 10;
    [SerializeField] BulletType bulletType;
    [SerializeField] float frequency = 10f;
    [SerializeField] float magnitude = .5f;
    [SerializeField] float scalingFactor = .05f;

    [SerializeField] ParticleSystem fireVFX;

    Vector3 startingPos;
    Vector3 currentPos;
    Vector3 axis;
    bool destroyAfterDistance = false;
    float distanceBeforeDestroy = 1f;
    BoxCollider projectileCollider;

    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 3f);
        Destroy(fireVFX, 5f);
        startingPos = transform.position;
        //GetComponent<Rigidbody>().useGravity = false;
        axis = transform.up;
        if(bulletType == BulletType.SpreadShot) {
            projectileCollider = GetComponent<BoxCollider>();
        }

    }

    // Update is called once per frame
    void Update() {
        if(bulletType == BulletType.SpreadShot) {
            ProcessSpreadShot();
        }
        else if(bulletType == BulletType.RailgunShot) {
            ProcessRailgunShot();
        }
    }

    private void ProcessSpreadShot() {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        currentPos = transform.position;
        transform.position = currentPos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        projectileCollider.size = new Vector3(projectileCollider.size.x + scalingFactor, projectileCollider.size.y, projectileCollider.size.z);
        ParticleSystem.ShapeModule particleShape = fireVFX.shape;
        particleShape.radius += scalingFactor;
    }

    private void ProcessRailgunShot() {
        currentPos = transform.position;
        if(destroyAfterDistance && (Vector3.Distance(startingPos, currentPos) >= distanceBeforeDestroy)) {
            fireVFX.GetComponent<ParticlePersist>().ParentAboutToBeDestroyed();
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
    }

    public int GetDamage() { return damagePerBullet; }

    public void SetDamage(int damage) { damagePerBullet = damage; }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("hit something with not raycast: " + other.transform.name);
        EnemyHealth enemyHealthComponent;
        if(enemyHealthComponent = other.GetComponentInParent<EnemyHealth>()) {
            enemyHealthComponent.TakeDamage(damagePerBullet);
        }
        //transform.DetachChildren();
        //Destroy(gameObject);
    }

    public void DestroyAfterDistanceTraveled(float distanceToTravel) {
        destroyAfterDistance = true;
        distanceBeforeDestroy = distanceToTravel;
    }
}
