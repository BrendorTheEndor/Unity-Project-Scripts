using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;

    Room roomToSpawnIn;

    private void Start() {
        roomToSpawnIn = GetComponentInParent<Room>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void SpawnEnemy() {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, roomToSpawnIn.transform);
        Destroy(gameObject);
    }
}
