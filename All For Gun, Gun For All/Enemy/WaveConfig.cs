using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab; // Enemy to spawn this wave
    [SerializeField] int numberToSpawn = 5;
    [SerializeField] float timeBetweenSpawns = .5f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public int GetNumberToSpawn() { return numberToSpawn; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

}
