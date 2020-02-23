using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    EnemySpawner[] enemySpawners;
    SpawnTrigger[] spawnTriggers;
    AITrigger[] aiTriggers;

    // Start is called before the first frame update
    void Start() {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
        spawnTriggers = GetComponentsInChildren<SpawnTrigger>();
        aiTriggers = GetComponentsInChildren<AITrigger>();
    }

    public void SpawnAll() {
        foreach(var spawner in enemySpawners) {
            spawner.SpawnEnemy();
        }
        foreach(var trigger in spawnTriggers) {
            Destroy(trigger.gameObject);
        }
    }

    public void TriggerAll() {
        EnemyAI[] enemyAIs = GetComponentsInChildren<EnemyAI>();
        foreach(var enemy in enemyAIs) {
            enemy.TriggerEnemy();
        }
        foreach(var trigger in aiTriggers) {
            Destroy(trigger.gameObject);
        }
    }
}
