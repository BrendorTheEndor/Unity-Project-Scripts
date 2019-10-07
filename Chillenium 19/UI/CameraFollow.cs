using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO Make there be a range the player can move around in before the camera moves
public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform playerTransform;

    float cameraStartingZOffset;
    //Quaternion cameraStartingRotation;

    // Start is called before the first frame update
    void Start() {
        cameraStartingZOffset = transform.position.z;
        //cameraStartingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, cameraStartingZOffset + playerTransform.position.z);
        //transform.rotation = cameraStartingRotation;
    }
}
