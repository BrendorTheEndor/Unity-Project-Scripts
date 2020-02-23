using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Standard scene loader/Application quit handler
public class LevelLoading : MonoBehaviour {

    [SerializeField] float GameOverDelay = 2f;

    public void LoadStartMenu() {
        SceneManager.LoadScene(0); // Must change if the start menu is no longer the first scene!
    }

    public void LoadGame() {
        SceneManager.LoadScene("Main Game"); // This also works, but it's not usually a great idea
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver() {
        StartCoroutine(DelayGameOver());
    }

    IEnumerator DelayGameOver() {
        yield return new WaitForSeconds(GameOverDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame() {
        Application.Quit();
    }

}
