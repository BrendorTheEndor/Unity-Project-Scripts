using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {

    PlayerHealth target;
    [SerializeField] int damageToDeal = 40;
    //[SerializeField] AudioClip attackSound;

    void Start() {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void PlayAttackSound() {
        //AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }

    public void AttackHitEvent() {
        if(target == null) { return; }
        Debug.Log("attack event triggered");

        // Makes sure you are actually still in range to get hit
        if(Vector3.Distance(transform.position, target.GetComponent<Transform>().position) <= GetComponentInParent<NavMeshAgent>().stoppingDistance) {
            target.TakeDamage(damageToDeal);
            Debug.Log("damage taken");
        }
        else {
            Debug.Log("attack whiffed");
        }
    }
}
