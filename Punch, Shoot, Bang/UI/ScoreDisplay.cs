using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField] TextMeshProUGUI scoreText;

    GameState myGameState;

    void Start() {
        myGameState = FindObjectOfType<GameState>();
    }

    void Update() {
        scoreText.text = "Score: " + myGameState.GetScore().ToString() + '\n' + "Combo: " + (myGameState.GetComboCount() - 1).ToString();
    }
}
