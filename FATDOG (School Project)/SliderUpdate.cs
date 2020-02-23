using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Keeps sliders updated with their values according to settings
public class SliderUpdate : MonoBehaviour {

    public enum SliderType { UI, Speed };

    private SliderType sliderType;
    private Slider slider;
    private SettingsValues settingsValues;

    private void Start() {
        slider = gameObject.GetComponent<Slider>();
        settingsValues = FindObjectOfType<SettingsValues>();
    }

    void Update() {
        // Protects against null reference
        if(settingsValues == null) { return; }

        if(sliderType == SliderType.UI) {
            slider.value = settingsValues.UiSize;
        }
        else if(sliderType == SliderType.Speed) {
            slider.value = settingsValues.gameSpeed;
        }
    }
}
