using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameState : MonoBehaviour {

    static public GameState instanceOfGameState;

    //[SerializeField] TextMeshProUGUI scoreText;
    //[SerializeField] Slider railgunPowerDisplay;
    [SerializeField] int maxRailgunDamage = 200;

    [SerializeField] public int playerScore;
    [SerializeField] int playerRailgunDamage = 1; // Only serialized for debugging

    private void Awake() {
        if(instanceOfGameState == null) {
            instanceOfGameState = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Start() {

        //if(railgunPowerDisplay == null) { return; }
        //scoreText.text = playerScore.ToString();
        //railgunPowerDisplay.value = (float)playerRailgunDamage / maxRailgunDamage;
    }

    public void AddScore(int scoreToAdd) {
        playerScore += scoreToAdd;
        //scoreText.text = playerScore.ToString();
    }

    public int GetRailgunDamage() { return playerRailgunDamage; }

    public int GetMaxRailgunDamage() { return maxRailgunDamage; }

    public void AddRailgunDamage() {
        if(playerRailgunDamage < maxRailgunDamage) {
            playerRailgunDamage++;
        }
        //railgunPowerDisplay.value = (float)playerRailgunDamage / maxRailgunDamage;

    }

    public void ResetRailgunDamage() {
        playerRailgunDamage = 1;
        //railgunPowerDisplay.value = (float)playerRailgunDamage / maxRailgunDamage;
    }

    public int GetScore() { return playerScore; }

    public void DestroyObject() { Destroy(gameObject); }
}
