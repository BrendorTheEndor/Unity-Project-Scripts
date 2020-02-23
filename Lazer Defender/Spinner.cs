using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    [SerializeField] float speedOfRotation = 1f;

    // Update is called once per frame
    void Update() {
        transform.Rotate(0f, 0f, speedOfRotation * Time.deltaTime); // Makes the projectile spin over time
    }
}
