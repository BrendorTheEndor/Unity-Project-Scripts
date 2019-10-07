using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplays : MonoBehaviour {

    WeaponSwitcher weaponSwitcher;

    // Start is called before the first frame update
    void Start() {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
    }

    void CanSwitch() {
        weaponSwitcher.CanSwitch();
    }
}
