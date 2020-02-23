using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    enum WeaponType { Railgun, Spread };

    [SerializeField] Projectile projectileToFire;
    [SerializeField] float timeBetweenShots = 1f;
    [SerializeField] Transform gunBarrel;
    [SerializeField] WeaponType weaponType;
    [SerializeField] float weaponRange = 100f;

    PlayerController myPlayerController;
    GameState myGameState;

    bool canFire = true;

    private void OnEnable() {
        canFire = true;
    }

    void Start() {
        myPlayerController = FindObjectOfType<PlayerController>();
        myGameState = GameState.instanceOfGameState;
    }

    void Update() {

        if(weaponType == WeaponType.Railgun) {
            projectileToFire.SetDamage(myGameState.GetRailgunDamage());
        }

        if(Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") > 0.0f) {
            if(weaponType == WeaponType.Railgun) {
                FireRailgun();
            }
            else if(weaponType == WeaponType.Spread) {
                FireSpread();
            }
        }
    }

    private void FireRailgun() {
        if(canFire) {
            if(myPlayerController.GetControllerBool()) {
                myPlayerController.RotateUsingController();
            }
            else {
                myPlayerController.FaceMouse();
            }

            var bulletFired = Instantiate(projectileToFire, gunBarrel.transform.position, transform.rotation);
            canFire = false;
            StartCoroutine(DelayFiring());

            RaycastHit hit;

            if(Physics.Raycast(gunBarrel.position, gunBarrel.forward, out hit, weaponRange)) {
                Debug.Log("hit something");
                bulletFired.DestroyAfterDistanceTraveled(hit.distance);

                EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
                if(target == null) {
                    myGameState.ResetRailgunDamage();
                    return;
                }

                Debug.Log("hit enemy");
                target.TakeDamage(projectileToFire.GetDamage());
                myGameState.ResetRailgunDamage(); // Put here so that it doesn't go down before the damage is processed
            }
        }
    }

    private void FireSpread() {
        if(canFire) {
            if(myPlayerController.GetControllerBool()) {
                myPlayerController.RotateUsingController();
            }
            else {
                myPlayerController.FaceMouse();
            }

            var bulletFired = Instantiate(projectileToFire, gunBarrel.transform.position, transform.rotation);
            canFire = false;
            StartCoroutine(DelayFiring());
        }
    }

    IEnumerator DelayFiring() {
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
    }
}
