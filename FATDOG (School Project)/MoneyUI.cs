using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class handles displaying the player's money and also the cheat code

public class MoneyUI : MonoBehaviour
{

    public Text moneyText;

    // Update is called once per frame
    void Update()
    {

        moneyText.text = "$" + PlayerStats.Money.ToString();

        // this is a cheat code which adds $100 when m is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerStats.Money += 1000;
        }

    }

}
