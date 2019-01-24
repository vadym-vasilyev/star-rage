using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyShip : EnemyShip {

    private GameSceneManager gameSceneManager;

    protected override void Start() {
        gameSceneManager = FindObjectOfType<GameSceneManager>();
        base.Start();
        healthOwner.OnHealthEnded += LaunchWinScene;
    }

    private void LaunchWinScene() {
        gameSceneManager.WinScreenDelayed();
    }
}
