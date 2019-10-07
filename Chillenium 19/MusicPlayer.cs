using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    static public MusicPlayer instanceOfMusicPlayer;
    private void Awake() {
        if(instanceOfMusicPlayer == null) {
            instanceOfMusicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
