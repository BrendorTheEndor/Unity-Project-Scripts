using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable Object used to spawn enemy waves
[CreateAssetMenu(menuName = "Enemy Wave Config")] // Makes this object creatable in the editor from the asset menu
public class WaveConfig : ScriptableObject {

    // Enemy type to spawn this wave
    [SerializeField] GameObject enemyPrefab;

    // The path the enemies follow once spawned
    [SerializeField] GameObject pathPrefab;

    // The time to wait between enemy spawns
    [SerializeField] float timeBetweenSpawns = 0.5f;

    // The range of time spawns can randomly differ by
    [SerializeField] float spawnRandomFactor = 0.3f;

    // Number of enemies to spawn this wave
    [SerializeField] int numberOfEnemies = 5;

    // The speed at which the enemies move along their path
    [SerializeField] float moveSpeed = 2f;

    // Getters
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }

    public List<Transform> getWaypoints() {

        var waveWaypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform) { // Do this thing to the transform of each child of the path; like iterating through array, but with children
            waveWaypoints.Add(child); // Add each child
        }

        return waveWaypoints;
    }

}
