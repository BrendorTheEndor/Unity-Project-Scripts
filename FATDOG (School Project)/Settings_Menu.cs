using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// this class is responsible for running the settings menu

public class Settings_Menu : MonoBehaviour {

    // set up the settings attributes
    public AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider maxLivesSlider;
    [SerializeField] Slider gameSpeedSlider;
    [SerializeField] Slider UISlider;
    [SerializeField] Slider hoverTimeSlider;

    [SerializeField] int maxFontColors = 4;

    // upon settings menu start
    void Start() {

        // Sets the sliders to the default values
        if(audioMixer != null) {
            float volume;
            volumeSlider.value = audioMixer.GetFloat("Volume", out volume) ? volume : 0;
        }
        if(SettingsValues.Instance != null) {
            maxLivesSlider.value = (SettingsValues.Instance.startLives + 1) / 2;
            gameSpeedSlider.value = SettingsValues.Instance.gameSpeed;
            UISlider.value = SettingsValues.Instance.UiSize;
        }

    }

    // handle volume control
    public void SetVolume(float volume) {

        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Volume", (int)volume);

    }

    // control the player's lives
    public void SetMaxLives(float lives) {

        int nLives = (int)(lives * 2 - 1);
        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.startLives = nLives;
        }
        if(PlayerStats.lostLives != null && PlayerStats.Lives != null) {
            int currentLives = nLives - PlayerStats.lostLives + PlayerStats.extraLives;
            PlayerStats.Lives = currentLives > 0 ? currentLives : 1; // Sets lives to new number of lives
        }

        PlayerPrefs.SetInt("Start_Lives", nLives);

    }

    // control the game speed
    public void SetGameSpeed(float speed) {
        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.gameSpeed = (int)speed;
        }
        PlayerPrefs.SetInt("Game_Speed", (int)speed);
    }

    // control the UI size
    public void SetUISize(float size) {
        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.UiSize = (int)size;
        }
        PlayerPrefs.SetFloat("UI_Size", (float)size);
    }

    // control the hover timer
    public void SetHoverTime(float time) {
        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.hoverTime = time;
        }
        PlayerPrefs.SetFloat("Hover_Time", time);
    }

    // control the volume
    public void modifyVolume(int amount) {

        float volume;
        audioMixer.GetFloat("Volume", out volume);
        volume = Mathf.Clamp(volume + amount, volumeSlider.minValue, volumeSlider.maxValue);
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Volume", (int)volume);
        volumeSlider.value = volume;

    }

    // contol the player's lives
    public void modifyLives(int amount) {

        int lives = (SettingsValues.Instance != null) ? SettingsValues.Instance.startLives : PlayerPrefs.GetInt("Start_Lives");
        lives = (int)Mathf.Clamp(lives + amount * 2, maxLivesSlider.minValue * 2 - 1, maxLivesSlider.maxValue * 2 - 1);
        PlayerPrefs.SetInt("Lives", (int)lives);
        maxLivesSlider.value = (lives + 1) / 2;

    }

    // control the game speed
    public void modifySpeed(int amount) {

        int speed = (SettingsValues.Instance != null) ? SettingsValues.Instance.gameSpeed : PlayerPrefs.GetInt("Game_Speed");

        speed = (int)Mathf.Clamp(speed + amount, gameSpeedSlider.minValue, gameSpeedSlider.maxValue);
        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.gameSpeed = speed;
        }
        PlayerPrefs.SetInt("Game_Speed", speed);
        gameSpeedSlider.value = speed;

    }

    // control the UI size
    public void modifyUI(float amount) {

        float uiSize = PlayerPrefs.GetFloat("UI_Size");

        uiSize = Mathf.Clamp(uiSize + amount, UISlider.minValue, UISlider.maxValue);

        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.UiSize = uiSize;
        }

        PlayerPrefs.SetFloat("UI_Size", uiSize);
        UISlider.value = uiSize;

    }

    // control the hover button
    public void modifyHover(int amount) {

        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.hoverOn = amount;
        }
        PlayerPrefs.SetInt("Hover_On", amount);

    }

    // control the hover timer
    public void modifyHoverTime(float amount) {

        float hoverTime = PlayerPrefs.GetFloat("Hover_Time");

        hoverTime = Mathf.Clamp(hoverTime + amount, hoverTimeSlider.minValue, hoverTimeSlider.maxValue);

        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.hoverTime = hoverTime;
        }

        PlayerPrefs.SetFloat("Hover_Time", hoverTime);
        hoverTimeSlider.value = hoverTime;

    }

    // control the font color button
    public void modifyFontColor() {

        int fontColor = PlayerPrefs.GetInt("Font_Color");

        if(fontColor + 1 >= maxFontColors) {
            fontColor = 0;
        }
        else {
            fontColor++;
        }

        if(SettingsValues.Instance != null) {
            SettingsValues.Instance.fontColor = fontColor;
        }

        PlayerPrefs.SetInt("Font_Color", fontColor);

    }

    // control adding extra lives
    public void AddLife() {
        PlayerStats.extraLives++;
        PlayerStats.Lives++;
    }
}