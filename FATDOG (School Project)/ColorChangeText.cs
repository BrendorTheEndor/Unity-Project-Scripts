using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sets this element's font color to the appropriate color based off of the settings
public class ColorChangeText : MonoBehaviour {

    Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    void Update() {
        if(SettingsValues.Instance.fontColor == 0) {
            text.color = Color.white;
        }
        else if(SettingsValues.Instance.fontColor == 1) {
            text.color = Color.black;
        }
        else if(SettingsValues.Instance.fontColor == 2) {
            text.color = Color.yellow;
        }
        else if(SettingsValues.Instance.fontColor == 3) {
            text.color = Color.green;
        }
    }
}
