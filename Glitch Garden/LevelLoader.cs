using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    [SerializeField] float timeToWait = 5f;

    int currentSceneIndex;

    void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 0) {
            StartCoroutine(WaitAndLoad());
        }
    }

    IEnumerator WaitAndLoad() {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void ReloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void LoadOptionsMenu() {
        SceneManager.LoadScene("Options Screen");
    }

}
