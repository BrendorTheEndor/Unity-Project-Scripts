using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        FindObjectOfType<LivesDisplay>().RemoveLife();
    }
}
