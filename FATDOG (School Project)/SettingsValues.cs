using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsValues : MonoBehaviour {
    public static SettingsValues Instance { get; private set; }
    public AudioMixer mixer;

    public int startLives;
    public int gameSpeed; //2x actual game speed
    public float UiSize;
    public int hoverOn;
    public float hoverTime;
    public int fontColor;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Adds PlayerPrefs if non-existent.
            if(!PlayerPrefs.HasKey("Volume")) {
                PlayerPrefs.SetInt("Volume", 80);
            }
            if(!PlayerPrefs.HasKey("Start_Lives")) {
                PlayerPrefs.SetInt("Start_Lives", 3);
            }
            if(!PlayerPrefs.HasKey("Game_Speed")) {
                PlayerPrefs.SetInt("Game_Speed", 2);
            }
            if(!PlayerPrefs.HasKey("UI_Size")) {
                PlayerPrefs.SetFloat("UI_Size", 1.0F);
            }
            if(!PlayerPrefs.HasKey("Hover_On")) {
                PlayerPrefs.SetInt("Hover_On", 1);
            }
            if(!PlayerPrefs.HasKey("Hover_Time")) {
                PlayerPrefs.SetFloat("Hover_Time", 1.0F);
            }
            if(!PlayerPrefs.HasKey("Font_Color")) {
                PlayerPrefs.SetInt("Font_Color", 0);
            }
            // Sets values to the PlayerPrefs
            mixer.SetFloat("Volume", PlayerPrefs.GetInt("Volume"));
            startLives = PlayerPrefs.GetInt("Start_Lives");
            gameSpeed = PlayerPrefs.GetInt("Game_Speed");
            UiSize = PlayerPrefs.GetFloat("UI_Size");
            hoverOn = PlayerPrefs.GetInt("Hover_On");
            hoverTime = PlayerPrefs.GetFloat("Hover_Time");
            fontColor = PlayerPrefs.GetInt("Font_Color");
        }
        else {
            Destroy(gameObject);
        }
    }

}