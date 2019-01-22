using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    void Awake() {
        if (FindObjectOfType<GameSceneManager>()) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
