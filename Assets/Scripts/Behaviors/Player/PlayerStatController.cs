using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatController : MonoBehaviour {

    private PlayerState playerState = new PlayerState(100, 0);

    private struct PlayerState {
        public int shield;
        public int score;

        public PlayerState(int shield, int score) {
            this.shield = shield;
            this.score = score;
        }
    }

    public delegate void ShieldValueChange(int shield);
    public delegate void ScoreValueChange(int score);

    public event ShieldValueChange OnShieldValueChange = delegate { };
    public event ScoreValueChange OnScoreValueChange = delegate { };

    public int GetShield => playerState.shield;
    public int GetScore => playerState.score;

    public void DecreaseShield(int amount) {
        playerState.shield -= amount;
        if (playerState.shield < 0) {
            playerState.shield = 0;
        }
        OnShieldValueChange(playerState.shield);
        OnScoreValueChange(playerState.score);
    }

    public void IncreaseShield(int amount) {
        playerState.shield += amount;
        if (playerState.shield > 100) {
            playerState.shield = 100;
        }
        OnShieldValueChange(playerState.shield);
    }

    public void IncreaseScore(int amount) {
        playerState.score += amount;
        OnScoreValueChange(playerState.score);
    }

    public void DecreaseScore(int amount) {
        playerState.score -= amount;
        if (playerState.score < 0) {
            playerState.score = 0;
        }
        OnScoreValueChange(playerState.score);
    }

}
