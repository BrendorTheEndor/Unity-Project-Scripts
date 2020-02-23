using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;

    private void Awake() {
        //Screen.SetResolution(432, 768, false, 60);
        SetUpSingleton();
    }

    private void SetUpSingleton() {
        int numberOfGameSessions = FindObjectsOfType(GetType()).Length;

        if(numberOfGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() { return score; }

    public void AddToScore(int scoreToAdd) { score += scoreToAdd; }

    public void ResetGame() { Destroy(gameObject); }

}
