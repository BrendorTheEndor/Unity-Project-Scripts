using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour {

    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Attacker[] attackersToSpawn;

    bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start() {
        while(spawn) {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    public void StopSpawning() {
        spawn = false;
    }

    private void SpawnAttacker() {

        var attackerToSpawn = attackersToSpawn[UnityEngine.Random.Range(0,attackersToSpawn.Length)];

        Attacker newAttacker = Instantiate(attackerToSpawn, transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform; // Parent of the new thing we made is the transform of it, makes them belong to the spawners
    }

    // Update is called once per frame
    void Update() {

    }
}
