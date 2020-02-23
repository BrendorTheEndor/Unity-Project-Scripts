using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour {

    [SerializeField] float baseLives = 3f;
    float lives;
    Text livesText;

    // Start is called before the first frame update
    void Start() {
        lives = baseLives - PlayerPrefsController.GetDifficulty();
        livesText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void RemoveLife() {
        lives--;
        UpdateDisplay();

        if(lives <= 0) {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }

}
