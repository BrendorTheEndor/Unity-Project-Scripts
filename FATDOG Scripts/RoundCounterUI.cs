using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class's sole purpose is to display "Wave: X/Y" on the screen

public class RoundCounterUI : MonoBehaviour
{
    public Text Round;
    public WaveSpawner myWaveSpawner;
    // Update is called once per frame
    void Update()
    {
        Round.text = "Wave " + PlayerStats.Rounds.ToString() + "/" + myWaveSpawner.GetNumWaves();
    }
}
