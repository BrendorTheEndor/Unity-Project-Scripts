using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour {

    enum WeaponType { Pistol, Shotgun };

    [Header("General Reference")]
    [SerializeField] Camera firstPersonCamera;
    [SerializeField] GameObject soundOrigin;

    [Header("Weapon Attributes")]
    [SerializeField] WeaponType weaponType = WeaponType.Pistol;
    [SerializeField] float weaponRange = 100f;
    [SerializeField] float minWeaponDamagePerShot = 10f;
    [SerializeField] float maxWeaponDamagePerShot = 10f;
    [SerializeField] float shotDeviationFactor = .05f;
    [SerializeField] float timeBetweenShots = 1f;
    [SerializeField] int magazineSize = 10;

    [Header("Effects")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] float fireVolume = 1f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] TextMeshProUGUI ammoDisplayText;

    WeaponSwitcher weaponSwitcher;
    //CurrentWeapon currentWeaponScript;
    Coroutine firingCorutine;
    Animator gunAnimator;
    bool canFire = true;
    bool isReloading = false;
    int leftInMagazine;

    const string RELOAD_TRIGGER = "Reload";

    private void Start() {
        leftInMagazine = magazineSize;
        weaponSwitcher = GetComponentInParent<WeaponSwitcher>();
        //playerAnimator = GetComponentInParent<Animator>();
        gunAnimator = GetComponent<Animator>();
        //currentWeaponScript = GetComponentInParent<CurrentWeapon>();
    }

    // Update is called once per frame
    void Update() {
        ammoDisplayText.text = leftInMagazine.ToString() + " / " + magazineSize.ToString();

        if(isReloading) { return; }

        if(Input.GetButtonDown("Fire1") && canFire) {
            weaponSwitcher.SetCanSwitch(false);

            if(leftInMagazine <= 0) {
                isReloading = true;
                playerAnimator.SetTrigger(RELOAD_TRIGGER);
                weaponSwitcher.SetCanSwitch(false);
                return;
            }

            PlayFX();
            canFire = false;
            StartCoroutine(DelayFire());
            if(weaponType == WeaponType.Pistol) {
                ProcessPistolShot();
            }
            else if(weaponType == WeaponType.Shotgun) {
                ProcessShotgunShot();
            }
        }
        if(Input.GetButtonDown("Reload")) {
            if(leftInMagazine == magazineSize) { return; }
            isReloading = true;
            playerAnimator.SetTrigger(RELOAD_TRIGGER);
            weaponSwitcher.SetCanSwitch(false);
        }

    }

    private void OnEnable() {
        canFire = true;
        isReloading = false;
    }

    private void PlayFX() {
        muzzleFlash.Play();
        //AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireVolume);
        AudioSource.PlayClipAtPoint(fireSFX, soundOrigin.transform.position, fireVolume);
        gunAnimator.SetTrigger("Fire");
    }

    private void ProcessPistolShot() {

        leftInMagazine--;

        // What we hit with the cast
        RaycastHit hit;

        // Gets a random Vector3 to add to the center of the screen, so that not all shots are 100% accurate
        Vector3 shotDeviation = new Vector3(UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor),
            UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor), UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor));

        // First is where to shoot the ray from, next is what direction, then what we hit, and finally the range
        if(Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward + shotDeviation, out hit, weaponRange)) { // If we hit something, and assigns this to "hit"
            CreateHitImpact(hit);
            var target = hit.transform.GetComponent<Health>();
            if(target == null) { return; } // protects against null reference
            target.TakeDamage(UnityEngine.Random.Range(minWeaponDamagePerShot, maxWeaponDamagePerShot)); // Simple damage spread
        }
    }

    private void ProcessShotgunShot() {

        leftInMagazine--;

        for(int i = 0; i < 6; i++) {
            // What we hit with the cast
            RaycastHit hit;

            // Gets a random Vector3 to add to the center of the screen, so that not all shots are 100% accurate
            Vector3 shotDeviation = new Vector3(UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor),
                UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor), UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor));

            // First is where to shoot the ray from, next is what direction, then what we hit, and finally the range
            if(Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward + shotDeviation, out hit, weaponRange)) { // If we hit something, and assigns this to "hit"
                CreateHitImpact(hit);
                var target = hit.transform.GetComponent<Health>();
                if(target != null) {
                    target.TakeDamage(UnityEngine.Random.Range(minWeaponDamagePerShot, maxWeaponDamagePerShot)); // Simple damage spread
                }
            }
        }
    }

    private void CreateHitImpact(RaycastHit hit) {
        // Make the hit effect, at the point where hit, in direction of normal of the hit
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }

    IEnumerator DelayFire() {
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
        weaponSwitcher.SetCanSwitch(true);
    }

    public void FinishReload() {
        leftInMagazine = magazineSize;
        canFire = true;
        isReloading = false;
        weaponSwitcher.SetCanSwitch(true);
    }
}
