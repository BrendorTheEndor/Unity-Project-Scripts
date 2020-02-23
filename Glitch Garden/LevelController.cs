using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float nextLevelDelay = 4f;

    int numberOfAttackers = 0;
    bool timerFinished = false;

    private void Start() {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }

    public void AttackerSpawned() {
        numberOfAttackers++;
    }

    public void TimerOver() {
        timerFinished = true;
        StopSpawners();
    }

    private void StopSpawners() {

        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();

        foreach(AttackerSpawner spawner in spawnerArray) {
            spawner.StopSpawning();
        }
    }

    public void AttackerKilled() {
        numberOfAttackers--;
        if((numberOfAttackers <= 0) && timerFinished) {
            StartCoroutine(HandleWinCondition());
        }
    }

   IEnumerator HandleWinCondition() {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().volume = PlayerPrefsController.GetMasterVolume();
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(nextLevelDelay);

        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition() {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }
}
