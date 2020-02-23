using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that is responsible for moving the enemies along their paths
public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
    List<Transform> waypoints; // Type is transform because we only need the position of the waypoints
    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start() {
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[waypointIndex].transform.position; // Start at first waypoint
    }

    // Update is called once per frame
    void Update() {
        MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }

    private void MoveEnemy() {
        if(waypointIndex <= waypoints.Count - 1) { // -1 to prevent OOB

            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime; // Again, to make movement framerate independent

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if(transform.position == targetPosition) {
                waypointIndex++;
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
