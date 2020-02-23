using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour {

    [SerializeField] Weapon[] weapons;

    Weapon currentWeapon;

    public void SetCurrentWeapon(int newWeaponIndex) {
        currentWeapon = weapons[newWeaponIndex];
    }

    public void FinishReload() {
        currentWeapon.FinishReload();
    }
}
