using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays music, uses singleton to persist across multiple scenes
public class MusicPlayer : MonoBehaviour {

    void Awake() {
        SetUpSingleton();
    }

    // Basically, if the object exists already, destroy this instance, else don't destroy on load
    private void SetUpSingleton() {
        if(FindObjectsOfType(GetType()).Length > 1) { // GetType() means "instance of this class"
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
