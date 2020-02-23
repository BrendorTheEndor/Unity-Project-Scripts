using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deprecated script
public class ShopItem : MonoBehaviour {

    public enum TurretType { Turret, Beamer, Missile };

    [SerializeField] TurretType turretType;

    Shop shop;

    private void Start() {
        shop = FindObjectOfType<Shop>();
    }

    //public void ClickBehavior() {
    //    if(turretType == TurretType.Turret) {
    //        shop.SelectStandardTurret();
    //    }
    //    else if(turretType == TurretType.Beamer) {
    //        shop.SelectLaserBeamer();
    //    }
    //    else if(turretType == TurretType.Missile) {
    //        shop.SelectMissileLauncher();
    //    }
    //}

}
