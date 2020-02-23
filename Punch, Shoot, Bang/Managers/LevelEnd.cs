using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LevelEnd : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            other.GetComponent<RigidbodyFirstPersonController>().enabled = false; // In here for cursor use
            FindObjectOfType<SceneLoader>().LoadVictoryScreen();
        }
    }

}
