using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// there is one waveSpawner per difficulty

public class WaveSpawner : MonoBehaviour
{

    // set up wave spawner attributes
    public Wave[] waves;
    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    private int waveNumber = 0;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public static int enemiesAlive;
    private bool spawnDone;

    // upon game start
    void Start()
    {
        spawnDone = true;
        enemiesAlive = 0;
    }

    // get the number of waves in a single difficulty
    public int GetNumWaves()
    {
        return waves.Length;
    }

    // Update is called once per frame
    void Update()
    {

        // do nothing if wave is in progress
        if(enemiesAlive > 0 || !spawnDone)
        {
            return;
        }

        // check if player has beaten the difficulty
        if (waveNumber == waves.Length)
        {
            PlayerStats.wonGame = true;
            this.enabled = false;
        }

        if(countdown <= 0f)
        {
            enemiesAlive = 0; //Ensures that the waves start with 0 enemies alive.
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        // update timer
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);

    }

    // spawn in the next wave of enemies
    IEnumerator SpawnWave()
    {

        spawnDone = false;
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            SpawnEnemy(wave.enemies[i]);
            yield return new WaitForSeconds(1f / wave.enemiesPerSecond);
        }
        waveNumber++;

        spawnDone = true;

    }

    // spawn in a single enemy of a wave
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }
}
