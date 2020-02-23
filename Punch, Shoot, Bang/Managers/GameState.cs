using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    static public GameState instanceOfGameState;

    [SerializeField] float comboTimer = 1.5f;

    int playerScore = 0;
    int comboCount = 1;
    // TODO There's probably a better way to do this, where I calculate the total at the end of the combo
    //bool comboActive = false;
    //int scoreThisCombo = 0;
    Coroutine comboTimerCoroutine = null;

    private void Awake() {
        if(instanceOfGameState == null) {
            instanceOfGameState = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddScore(int scoreToAdd) {
        //playerScore += scoreToAdd;
        playerScore += scoreToAdd * comboCount;
        //scoreThisCombo += scoreToAdd;
        //comboActive = true;
        comboCount++;
        // Resets the timer
        if(comboTimerCoroutine != null) {
            StopCoroutine(comboTimerCoroutine);
        }
        comboTimerCoroutine = StartCoroutine(ScoreComboTimer());
    }

    public int GetScore() { return playerScore; }

    public int GetComboCount() { return comboCount; }

    IEnumerator ScoreComboTimer() {
        yield return new WaitForSeconds(comboTimer);
        //comboActive = false;
        comboCount = 1;
        comboTimerCoroutine = null;
    }
}
