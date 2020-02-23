using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrigger : MonoBehaviour {

    Room parentRoom;

    private void Start() {
        parentRoom = GetComponentInParent<Room>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            parentRoom.TriggerAll();
        }
    }
}
