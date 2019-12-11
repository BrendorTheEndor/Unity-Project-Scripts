using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverClick : MonoBehaviour {

    GameObject currentlySelectedElement = null;
    SettingsValues settingsValues;

    private void Start() {
        settingsValues = FindObjectOfType<SettingsValues>();
    }

    void Update() {
        if(settingsValues.hoverOn != 1) { return; }

        if(!CheckMouseHover()) { // If you exit a button
            if(currentlySelectedElement != null) {
                currentlySelectedElement.GetComponent<ClickableElement>().MouseExit();
            }

            currentlySelectedElement = null;
        }
    }

    // Checks if the mouse has hit something
    private bool CheckMouseHover() {

        // Gets all the objects the mouse is hovering over
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        foreach(var result in raycastResultList) {

            if(result.gameObject.GetComponent<ClickableElement>()) { // If we hover a button

                if(currentlySelectedElement == null) { // If first entering a button, start selection process
                    currentlySelectedElement = result.gameObject;
                    result.gameObject.GetComponent<ClickableElement>().MouseEnter();
                    return true;
                }

                if(result.gameObject != currentlySelectedElement) { // If what button hovering changed, reset selection process
                    currentlySelectedElement.GetComponent<ClickableElement>().MouseExit();
                    currentlySelectedElement = null;
                    return true;
                }

                return true;
            }
        }

        return false;
    }
}
