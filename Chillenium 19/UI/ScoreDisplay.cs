using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameState gameState;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Start called");
        scoreText = GetComponent<TextMeshProUGUI>();
        Debug.Log("get here");
        gameState = GameState.instanceOfGameState;
        Debug.Log(gameState.transform.name);
        if(gameState == null) {
            Debug.Log("Game state is null");
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("update");
        //scoreText.text = gameState.GetScore().ToString();
        scoreText.text = gameState.playerScore.ToString();
        Debug.Log(gameState.GetScore().ToString());
    }
}
