using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene(PredefinedStrings.SCENE_GAME);
    }

    public void Settings() {
        SceneManager.LoadScene(PredefinedStrings.SCENE_SETTINGS);
    }

    public void MainMenu() {
        SceneManager.LoadScene(PredefinedStrings.SCENE_MENU);
    }

    public void QuitGame() {
        Application.Quit();
    }

}
