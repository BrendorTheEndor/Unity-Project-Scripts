using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    // set up enemy attributes

    [SerializeField] GameObject spriteRenderer;
    Quaternion startRotation;

    public float startSpeed = 10f;
    public float startHealth = 100;
    public float health;
    public int moneyValue = 25;
    public GameObject deathEffect;
    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;
    private float speed;

    // Start is called before the first frame update
    void Start() {
        target = Waypoints.waypoints[0];
        startRotation = spriteRenderer.transform.rotation;
        speed = startSpeed;
        health = startHealth;
    }

    // when a pill/laser/carrot hits a dog, lower its health
    public void TakeDamage (float amount)
    {

        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            health = 100000000; // Not a proper mutex, but it's good enough for now :/
            Die();
        }

    }

    // a dog has been thinned down so remove it from screen
    void Die ()
    {

        PlayerStats.Money += moneyValue;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;

    }

    // Update is called once per frame
    void Update() {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.3f) {
            GetNextWaypoint();
        }

        spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(90, transform.rotation.y, -Vector3.Angle(dir, new Vector3(1, 0, 0))));
        speed = startSpeed;

    }

    // set the dog's next target to move towards
    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoints.waypoints.Length -1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }

    // the dog reached the food bowl/end/red cube
    void EndPath ()
    {
        PlayerStats.Lives--;
        PlayerStats.lostLives++;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;
    }
    
    // the laser beamer causes dogs to slow down
    public void Slow(float slowPercent)
    {
        speed = startSpeed * (1f - slowPercent);
    }

}
