using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("General")]
    [SerializeField] float health = 100;

    [Header("Projectile")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] AudioClip fireSFX;
    [Range(0f, 1f)] [SerializeField] float fireVolume = 1f;

    [Header("On Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionLiveTime = 1f;
    [SerializeField] AudioClip deathSFX;
    [Range(0f, 1f)] [SerializeField] float deathVolume = 1f;
    [SerializeField] int scoreOnKill = 50;

    GameSession gameSession;

    // State
    float shotCounter;

    // Start is called before the first frame update
    void Start() {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update() {
        CountDownAndShoot();
    }

    private void CountDownAndShoot() {
        shotCounter -= Time.deltaTime; // Framerate independent
        if(shotCounter <= 0) {
            Shoot();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Shoot() {
        GameObject enemyShot = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        enemyShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
        GameObject explosionEffect = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosionEffect, explosionLiveTime);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        gameSession.AddToScore(scoreOnKill);
    }
}
