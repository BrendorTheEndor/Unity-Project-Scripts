using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour {

    [SerializeField] float damageToDeal = 20;

    //PlayerMelee player;

    //// Start is called before the first frame update
    //void Start() {
    //    player = FindObjectOfType<PlayerMelee>();
    //}

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            //Debug.Log("player was hit");
            other.GetComponent<Health>().TakeDamage(damageToDeal);
        }
    }

}
