using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Health : MonoBehaviour {

    public enum HealthType { Player, Enemy, Miniboss };

    [SerializeField] float maxHealth = 100f;
    [Tooltip("Can be either the explosion for an enemy or the player's death canvas")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] HealthType healthType = HealthType.Player;
    float hitPoints;

    [Header("For Player")]
    [SerializeField] float healthRecoveryPerFrame = 0f;
    [SerializeField] float healthRegenTimer = 1.0f;
    [SerializeField] float invincibilityTimer = .5f;
    [SerializeField] float gameOverTimer = 1.5f;
    [SerializeField] Slider playerHealthbar;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float deathVolume = 1f;
    bool canTakeDamage = true;
    bool canRecoverHealth = false;
    Coroutine healthRegenTimerCoroutine = null;

    [Header("For Enemies")]
    [SerializeField] int baseScoreValue = 50;
    [SerializeField] AudioClip[] explosionSFX;
    [SerializeField] float explosionVolume = 1f;
    [SerializeField] float despawnTimer = 5f;
    bool isDead = false;
    bool canExoplode = false; // Used to explode when airborne or stunned

    const string DAMAGE_METHOD = "OnDamageTaken";
    const string EXPLODE_TRIGGER = "Explode";
    const string DIE_TRIGGER = "Die";

    private void Start() {
        if(healthType == HealthType.Player) {
            deathEffect.SetActive(false);
            canRecoverHealth = true;
        }
        hitPoints = maxHealth;
        if(playerHealthbar) {
            playerHealthbar.value = hitPoints / maxHealth;
        }
    }

    private void Update() {
        if(canRecoverHealth) {
            hitPoints = Mathf.Clamp(hitPoints + healthRecoveryPerFrame * Time.deltaTime, 0.0f, maxHealth);
        }
        if(healthType == HealthType.Player) {
            playerHealthbar.value = hitPoints / maxHealth;
        }
    }

    public void TakeDamage(float damageToTake, bool causeExplosion = false) {

        if(isDead) { return; }

        if(healthType == HealthType.Player && canTakeDamage) {
            //Debug.Log("Player takes " + damageToTake + " damage");
            hitPoints -= damageToTake;
            StartCoroutine(InvicibilityTimer());
            if(healthRegenTimerCoroutine == null) {
                healthRegenTimerCoroutine = StartCoroutine(DelayHealthRegen());
            }
            else {
                StopCoroutine(healthRegenTimerCoroutine);
                healthRegenTimerCoroutine = StartCoroutine(DelayHealthRegen());
            }
        }


        if(healthType == HealthType.Enemy || healthType == HealthType.Miniboss) {
            hitPoints -= damageToTake;
            BroadcastMessage(DAMAGE_METHOD); // Calls this method on this game object or children
        }

        if(hitPoints <= 0) {
            isDead = true;
            if(healthType == HealthType.Player) {
                Debug.Log("Player has died");
                playerHealthbar.fillRect.transform.localScale = new Vector3(0, 0, 0);
                deathEffect.SetActive(true);
                GetComponent<RigidbodyFirstPersonController>().enabled = false;
                AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
                StartCoroutine(WaitAndLoad());
            }
            else {
                Die(causeExplosion);
            }
        }
    }

    private void Die(bool causeExplosion) { // This arg is probably overkill
        if(causeExplosion || canExoplode) {
            GetComponent<Animator>().SetTrigger(EXPLODE_TRIGGER);
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else {
            GetComponent<Animator>().SetTrigger(DIE_TRIGGER);

            foreach(var coll in GetComponents<Collider>()) {
                coll.enabled = false;
            }
            foreach(var coll in GetComponentsInChildren<Collider>()) {
                coll.enabled = false;
            }

            Destroy(gameObject, despawnTimer);
        }
        GetComponent<EnemyAI>().enabled = false;
    }

    public void Explode() {
        GameObject explosion = Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSFX[UnityEngine.Random.Range(0, explosionSFX.Length - 1)], transform.position, explosionVolume);
        Destroy(explosion, 1f);
        FindObjectOfType<GameState>().AddScore(baseScoreValue);
        Destroy(gameObject);
    }

    //public bool IsDead() { return isDead; }

    public void SetCanExplode(bool value) {
        canExoplode = value;
    }

    public void AddHealth(float healthToAdd) {
        hitPoints = Mathf.Clamp(hitPoints + healthToAdd, 0, maxHealth);
        playerHealthbar.value = hitPoints / maxHealth;
    }

    public HealthType GetHealthType() { return healthType; }

    // Prevents the player from being hit again in a short timeframe
    IEnumerator InvicibilityTimer() {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityTimer);
        canTakeDamage = true;
    }

    IEnumerator DelayHealthRegen() {
        canRecoverHealth = false;
        yield return new WaitForSeconds(healthRegenTimer);
        canRecoverHealth = true;
        healthRegenTimerCoroutine = null;
    }

    IEnumerator WaitAndLoad() {
        yield return new WaitForSeconds(gameOverTimer);
        Debug.Log("load death screen");
        FindObjectOfType<SceneLoader>().LoadDeathScreen();
    }
}
