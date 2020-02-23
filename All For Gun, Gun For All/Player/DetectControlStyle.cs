using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlStyle : MonoBehaviour {

    PlayerController playerController;

    void Start() {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update() {
        // Detect mouse input
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)) {
            playerController.SetControllerBool(false);
        }
        if(Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f) {
            playerController.SetControllerBool(false);
        }

        // Detect Controller Input
        if(Input.GetAxisRaw("RHorizontal") != 0.0f || Input.GetAxisRaw("RVertical") != 0.0f) {
            playerController.SetControllerBool(true);
        }
    }
}
