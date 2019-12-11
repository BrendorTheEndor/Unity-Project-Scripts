using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class allows the player's lives to be displayed on the screen

public class LivesUI : MonoBehaviour
{

    public Text livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.Lives + " LIVES";
    }
}
