using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> wavesToSpawn;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loopWaves = false;
    [SerializeField] float radiusOfRandomSpawn = 10f;

    IEnumerator Start() {
        do {
            yield return StartCoroutine(SpawnAllWaves());
        } while(loopWaves);
    }

    private IEnumerator SpawnAllWaves() {
        for(int i = startingWave; i < wavesToSpawn.Count; i++) {
            yield return StartCoroutine(SpawnWaveNumber(i));
        }
    }

    public IEnumerator SpawnWaveNumber(int waveNumberToSpawn) {
        for(int i = 0; i < wavesToSpawn[waveNumberToSpawn].GetNumberToSpawn(); i++) {
            Vector3 spawnLocation = new Vector3(UnityEngine.Random.Range(transform.position.x - radiusOfRandomSpawn, transform.position.x + radiusOfRandomSpawn),
                transform.position.y, UnityEngine.Random.Range(transform.position.z - radiusOfRandomSpawn, transform.position.z + radiusOfRandomSpawn));
            Instantiate(wavesToSpawn[waveNumberToSpawn].GetEnemyPrefab(), spawnLocation, Quaternion.identity);

            yield return new WaitForSeconds(wavesToSpawn[waveNumberToSpawn].GetTimeBetweenSpawns());
        }
    }
}
