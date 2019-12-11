using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static float currentTimeScale;

    // load the menu scene (from startup, pause menu, and settings menu)
    public void loadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
        
    }

    // load the easy scene (from main menu)
    public void loadEasyScene()
    {
        Time.timeScale = SettingsValues.Instance.gameSpeed;
        SceneManager.LoadScene("EasyScene");

    }

    // load the medium scene (from main menu)
    public void loadMediumScene()
    {
        Time.timeScale = SettingsValues.Instance.gameSpeed;
        SceneManager.LoadScene("MediumScene");

    }

    // load the hard scene (from main menu)
    public void loadHardScene()
    {
        Time.timeScale = SettingsValues.Instance.gameSpeed;
        SceneManager.LoadScene("HardScene");

    }

}
