using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadTitleScene() {
        SceneManager.LoadScene(0);
    }

    public void LoadVictoryScreen() {
        SceneManager.LoadScene("Victory Screen");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadDeathScreen() {
        SceneManager.LoadScene("Death Screen");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitApplication() {
        Application.Quit();
    }
}
