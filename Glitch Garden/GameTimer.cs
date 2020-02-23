using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    [Tooltip("Our level timer in seconds")] [SerializeField] float levelTime = 10f;
    bool triggeredLevelFinish = false;

    void Update() {

        if(triggeredLevelFinish) { return; }

        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime; // Makes the slider move over time

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);

        if(timerFinished) {
            FindObjectOfType<LevelController>().TimerOver();
            triggeredLevelFinish = true;
        }
    }
}
