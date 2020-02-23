using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Placed on object that can be clicked, so that it works with hover only
public class ClickableElement : MonoBehaviour {

    // Object with the "click" method on it
    GameObject LogicObject;
    SettingsValues settingsValues;
    Button buttonComponent;

    Coroutine timerCoroutine = null;
    float selectionTimer = 1f;

    private void Awake() {
        LogicObject = gameObject;
        settingsValues = FindObjectOfType<SettingsValues>();
        buttonComponent = GetComponent<Button>();
    }

    private void Update() {
        selectionTimer = settingsValues.hoverTime;
    }

    public void SetSelectionTimer(float value) {
        selectionTimer = value;
    }

    // For non-UI elements
    private void OnMouseEnter() {
        MouseEnter();
    }

    private void OnMouseExit() {
        MouseExit();
    }

    // Called for UI elements
    public void MouseEnter() {
        timerCoroutine = StartCoroutine(ButtonPressTimer());
    }

    public void MouseExit() {
        StopCoroutine(timerCoroutine);
        timerCoroutine = null;
    }

    private void Click() {
        if(buttonComponent == null) {
            // Custom made
            LogicObject.BroadcastMessage("ClickBehavior");
        }
        else {
            // For precanned buttons
            buttonComponent.BroadcastMessage("Press");
        }
    }

    IEnumerator ButtonPressTimer() {
        yield return new WaitForSecondsRealtime(selectionTimer);
        if(settingsValues.hoverOn == 1) {
            Click();
        }
    }

}
