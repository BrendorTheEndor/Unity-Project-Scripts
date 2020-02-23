using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages all the sizeable UI elements
public class SizeableUIParent : MonoBehaviour {

    [SerializeField] Slider UISizeSlider;
    [SerializeField] SizeableUI[] sizeableUIElements;

    void Start() {
        foreach(var element in sizeableUIElements) {
            element.SetSlider(UISizeSlider);
        }
        updateUISize();
        if(SettingsValues.Instance != null) {
            UISizeSlider.value = SettingsValues.Instance.UiSize;
        }
    }

    public void updateUISize() {
        foreach(var element in sizeableUIElements) {
            element.ResizeUI();
        }
    }
}
