using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Placed on UI element that can be resized
public class SizeableUI : MonoBehaviour {

    [SerializeField] float inwardsMoveAmountX = 1f;
    [SerializeField] float inwardsMoveAmountY = 1f;

    Slider UISizeSlider;
    Vector3 UISize;
    RectTransform rectTransform;
    Vector3 initialPosition;
    float lastSliderVal;
    Text textElement = null;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        UISize = rectTransform.localScale;
        initialPosition = rectTransform.position;
        textElement = GetComponent<Text>();
    }

    private void Update() {

        // Set the size equal to what it is calculated as
        rectTransform.localScale = UISize;

        // If size is increased
        if(lastSliderVal - UISizeSlider.value <= 0) {
            rectTransform.position = initialPosition + new Vector3(inwardsMoveAmountX * UISizeSlider.value, inwardsMoveAmountY * UISizeSlider.value, 0f);
        }
        // If size is decreased
        else {
            rectTransform.position = initialPosition - new Vector3(inwardsMoveAmountX * UISizeSlider.value, inwardsMoveAmountY * UISizeSlider.value, 0f);
        }
    }

    public void SetSlider(Slider slider) {
        UISizeSlider = slider;
        lastSliderVal = UISizeSlider.value;
    }

    public void ResizeUI() {
        UISize = new Vector3(UISizeSlider.value, UISizeSlider.value, rectTransform.localScale.z);
        lastSliderVal = UISizeSlider.value;
    }
}
