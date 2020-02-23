using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour {

    [SerializeField] Slider timeScaleSlider;

    public static bool isPaused;
    public float currentTimeScale; // EXISTS FOR DEBUGGING PURPOSES, USE SettingsValues.Instance.gameSpeed INSTEAD

    public GameObject pauseMenuUI;
    public TextMeshProUGUI pauseButtonText;
    
    private void Start() {

        isPaused = false; // Sets pause to false upon startup
        if (SettingsValues.Instance != null) {
            Time.timeScale = SettingsValues.Instance.gameSpeed / 2.0f;
        }
        currentTimeScale = Time.timeScale; 
        timeScaleSlider.value = Time.timeScale * 2;

    }
    
    //Handles when the game is paused
    public void PauseUnpause() {

        if (isPaused) { // Game currently paused, will unpause
            isPaused = false;
            pauseMenuUI.SetActive(false);
            pauseButtonText.SetText("Pause");
            if (SettingsValues.Instance != null) { 
                Time.timeScale = SettingsValues.Instance.gameSpeed / 2.0f;
            }
            else { // Only if debugging from game scene
                Time.timeScale = currentTimeScale;
            }
        }
        else { // Game currently unpaused, will pause.
            isPaused = true;
            pauseMenuUI.SetActive(true);
            pauseButtonText.SetText("Unpause");
            if (SettingsValues.Instance != null) {
                SettingsValues.Instance.gameSpeed = (int)(Time.timeScale*2);
            }
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

    }

    public void SetTimeScale() {

        float timeScale = timeScaleSlider.value / 2.0f;
        currentTimeScale = timeScale;
        if (SettingsValues.Instance != null) { 
            SettingsValues.Instance.gameSpeed = (int)(timeScaleSlider.value);
        }
        if(!isPaused) {
            Time.timeScale = timeScale;
        }

    }

}
