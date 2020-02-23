using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class contains member variables which define a wave
// the number and type of enemies plus enemies per second define a wave

[System.Serializable]
public class Wave
{

    public GameObject[] enemies;
    public float enemiesPerSecond;

}
