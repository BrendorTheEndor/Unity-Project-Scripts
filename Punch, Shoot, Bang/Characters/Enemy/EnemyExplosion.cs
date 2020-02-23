using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour {

    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float randomDamageToDealMin = 100f;
    [SerializeField] float randomDamageToDealMax = 100f;
    [SerializeField] float minActualDamage = 10f;
    [SerializeField] float explosionForceMin = 3f;
    [SerializeField] float explosionForceMax = 3f;
    [SerializeField] float upwardsModifierMin = 1.0f;
    [SerializeField] float upwardsModifierMax = 1.0f;

    void Start() {
        var collisions = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(var collider in collisions) {
            Health health = null;
            if(health = collider.GetComponent<Health>()) {

                float distanceFromExplosion = Vector3.Distance(collider.transform.position, transform.position);
                float baseDamage = UnityEngine.Random.Range(randomDamageToDealMin, randomDamageToDealMin);
                float actualDamage = Mathf.Clamp(baseDamage * (1 - (distanceFromExplosion / explosionRadius)) + minActualDamage, minActualDamage, randomDamageToDealMax);

                //Debug.Log("Explosion hit " + collider.transform.name + " from " + distanceFromExplosion + " away and did " + actualDamage + " damage");

                health.TakeDamage(actualDamage, true);
                float explosionForce = Random.Range(explosionForceMin, explosionForceMax);
                float upwardsModifier = Random.Range(upwardsModifierMin, upwardsModifierMax);
                collider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }
    }

    // Used to show the chase range in the editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
