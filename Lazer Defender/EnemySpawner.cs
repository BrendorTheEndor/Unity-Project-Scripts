using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs; // Gets a list of waves
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loopWaves = false;

    // Start is called before the first frame update
    IEnumerator Start() {
        do { // Does once no matter what, if looping then it loops infinitely
            yield return StartCoroutine(SpawnAllWaves());
        } while(loopWaves);
    }

    private IEnumerator SpawnAllWaves() {

        for(int i = startingWave; i < waveConfigs.Count; i++) {

            var currentWave = waveConfigs[i]; // Get the current wave

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave)); // Suspend operation until all the enemies have spawned in that wave
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {

        for(int i = 0; i < waveConfig.GetNumberOfEnemies(); i++) { // Iterate through and spawn the amount of enemies we want to spawn

            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.getWaypoints()[0].transform.position, Quaternion.identity); // Get the enemy to spawn, spawn it at the first waypoint; that's why [0]

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig); // Gets the enemy pathing script to set the path to

            // The method call above must happen in EnemyPathing.cs before that script's start method. The reason that works here is because
            // everything in here is called BEFORE the first frame of the EnemyPathing existence, whereas start is called ON the first frame of
            // existence (start is the next frame after creation, awake is IMMEDIATE)

            yield return new WaitForSeconds(UnityEngine.Random.Range(waveConfig.GetTimeBetweenSpawns() - waveConfig.GetSpawnRandomFactor(),
                waveConfig.GetTimeBetweenSpawns() + waveConfig.GetSpawnRandomFactor())); // Waits a random time deviating by the inputted amount
        } // For loop ends here so there is a delay between spawns
    }
}
