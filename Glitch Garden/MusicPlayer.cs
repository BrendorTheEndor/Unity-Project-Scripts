using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject); // No singleton, because we're only playing once
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
