using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeaponSwitcher : MonoBehaviour {

    [SerializeField] GameObject weaponDisplays;

    Animator weaponDisplaysAnimator;

    bool canSwitch = true;
    int currentWeapon = 0;

    void Start() {
        weaponDisplaysAnimator = weaponDisplays.GetComponent<Animator>();
        SetWeaponActive();
    }

    void Update() {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheelInput();

        if(canSwitch && (previousWeapon != currentWeapon)) {
            weaponDisplaysAnimator.SetTrigger("WeaponSwitch");
            SetWeaponActive();
        }
    }

    private void ProcessKeyInput() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            currentWeapon = 1;
        }
        else if(Input.GetButtonDown("Switch")) {
            if(currentWeapon >= transform.childCount - 1) { // Loop around if needed
                currentWeapon = 0;
            }
            else {
                currentWeapon++;
            }
        }
    }

    private void ProcessScrollWheelInput() {
        if(Input.GetAxis("Mouse ScrollWheel") > 0) {
            if(currentWeapon >= transform.childCount - 1) { // Loop around if needed
                currentWeapon = 0;
            }
            else {
                currentWeapon++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0) {
            if(currentWeapon <= 0) { // Loop around if needed
                currentWeapon = transform.childCount - 1;
            }
            else {
                currentWeapon--;
            }
        }
    }

    private void SetWeaponActive() {
        canSwitch = false;
        int weaponIndex = 0;
        foreach(Transform weapon in transform) { // each child
            if(weaponIndex == currentWeapon) {
                weapon.gameObject.SetActive(true);
            }
            else {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    public void CanSwitch() { canSwitch = true; }
}
