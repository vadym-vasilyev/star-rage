﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameSceneManager : MonoBehaviour {

    private void Update() {
        if (CrossPlatformInputManager.GetButtonDown("Cancel")) {
            if (SceneManager.GetActiveScene().name == PredefinedStrings.SCENE_MENU) {
                QuitGame();
            } else {
                MainMenu();
            }
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(PredefinedStrings.SCENE_GAME);
    }

    public void Settings() {
        SceneManager.LoadScene(PredefinedStrings.SCENE_SETTINGS);
    }

    public void MainMenu() {
        SceneManager.LoadScene(PredefinedStrings.SCENE_MENU);
    }

    public void LooseScreenDelayed() {
        StartCoroutine(WaitAndDo(4f, () => { SceneManager.LoadScene(PredefinedStrings.SCENE_LOOSE); }));
    }

    public void WinScreenDelayed() {
        StartCoroutine(WaitAndDo(4f, () => { SceneManager.LoadScene(PredefinedStrings.SCENE_WIN); }));
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator WaitAndDo(float time, Action action) {
        yield return new WaitForSeconds(time);
        action();
    }

}
