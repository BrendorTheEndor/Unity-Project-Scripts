using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] float hitPoints = 100f;
    //[SerializeField] GameObject hitEffect;

    bool isDead = false;

    const string DAMAGE_METHOD = "OnDamageTaken";
    const string DIE_TRIGGER = "Die";

    public void TakeDamage(float damageToTake) {
        Debug.Log("Player takes " + damageToTake + " damage");
        hitPoints -= damageToTake;

        //BroadcastMessage(DAMAGE_METHOD); // Calls this method on this game object or children

        if(hitPoints <= 0) {
            Die();
        }
    }

    private void Die() {
        if(isDead) { return; }
        Debug.Log("Player has died");
        //GetComponent<CapsuleCollider>().enabled = false; // They're really big, so this is to prevent the bodies from getting in the way
        //GetComponent<Animator>().SetTrigger(DIE_TRIGGER);
        //isDead = true;
        //GameObject impact = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 1f);
        //Destroy(gameObject);
    }

    public bool IsDead() { return isDead; }
}
