using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    [SerializeField] float healthToAdd = 50f;

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            Debug.Log("Player picked up health");
            other.GetComponent<Health>().AddHealth(healthToAdd);
            Destroy(gameObject);
        }
    }

}
