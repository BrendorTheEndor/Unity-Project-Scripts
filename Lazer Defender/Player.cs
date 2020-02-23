using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Config Params
    [Header("Player")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float xEdgePadding = 1f; // Amount of padding to clamp player to screen edge by
    [SerializeField] float yEdgePadding = 1f;
    [SerializeField] int health = 100;

    [Header("Projectile")]
    [SerializeField] GameObject playerProjectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float timeBetweenShots = 0.1f;
    [SerializeField] AudioClip fireSFX;
    [Range(0f, 1f)] [SerializeField] float fireVolume = 1f;

    [Header("On Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionLiveTime = 1f;
    [SerializeField] AudioClip deathSFX;
    [Range(0f, 1f)] [SerializeField] float deathVolume = 1f;

    Coroutine firingCorutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    LevelLoading levelLoader;

    // Start is called before the first frame update
    void Start() {
        levelLoader = FindObjectOfType<LevelLoading>();
        setMoveBounds();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Fire();
    }

    // Uses a coroutine to auto fire as long as the button is held down
    private void Fire() {
        if(Input.GetButtonDown("Fire1")) {
            firingCorutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCorutine);
        }
    }

    // Instantiates a projectile, loops at the set fire rate until button is let go (shots are shredded once off screen, so are enemies)
    IEnumerator FireContinuously() {
        while(true) { // Infinite loop to always fire while button held
            GameObject playerShot = Instantiate(playerProjectile, transform.position, Quaternion.identity) as GameObject; // Last arg means "rotation as is"
            playerShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireVolume);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    // Uses a DamageDealer script placed on objects that deal damage to process damage taken
    private void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; } // Avoids null reference; ie if there isn't damage to be dealt, then don't do anyhting
        ProcessHit(damageDealer);
    }

    // Deals the damage and destroys the damage dealer, also kills player if health has hit 0
    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0) {
            Die();
        }
    }

    // Processes the player's death
    private void Die() {
        // Get rid of the player object
        Destroy(gameObject);

        // Create explosion VFX
        GameObject explosionEffect = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosionEffect, explosionLiveTime);

        // Play the death SFX
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);

        // Load the game over scene
        levelLoader.LoadGameOver();
    }

    private void Move() { // Just controls moving around the player

        // Time.deltaTime is the time, in seconds, each frame takes at runtime; multiplying by this makes the movement speed framerate independent (same m/s @ any fps)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed; // The "axis" is in the project settings, and it makes controls easier
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); // "var" is just easier than "float" to type

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    // Sets the max distance on the screen the player can move
    private void setMoveBounds() {
        Camera gameCam = Camera.main; // Get our main camera

        xMin = gameCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + xEdgePadding; // Give the position in 1X1 relation to screen, converts to actual units according to cam, used in case we change cam size
        xMax = gameCam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - xEdgePadding; // Gets the max area to move to, which is 1

        yMin = gameCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + yEdgePadding;
        yMax = gameCam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - yEdgePadding;
    }

    // Gets the player health
    public int getHealth() { return health; }
}
