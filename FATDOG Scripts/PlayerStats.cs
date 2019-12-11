using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class handles the player's attributes like lives, money, etc.

public class PlayerStats : MonoBehaviour
{

    // set up player's starting attributes
    public static int Money;
    public int startMoney = 100;

    public static int lostLives;
    public static int Lives;
    public static int extraLives;

    public static int Rounds;
    public static bool wonGame;

    // upon game start, make sure starting attributes are appropriate
    void Start()
    {

        wonGame = false;
        Money = startMoney;
        lostLives = 0;
        extraLives = 0;
        if (SettingsValues.Instance != null)
        {
            Lives = SettingsValues.Instance.startLives;
        }
        else
        {
            Lives = 3;
        }
        Rounds = 0;

    }

}
