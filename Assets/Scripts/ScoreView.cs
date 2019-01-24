using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {
    private Text scoreText;
    private PlayerStatController playerStatHandler;

    void Start() {
        playerStatHandler = FindObjectOfType<PlayerStatController>();
        scoreText = GetComponent<Text>();
        playerStatHandler.OnScoreValueChange += OnScoreValueChange;
        scoreText.text = playerStatHandler.GetScore.ToString();
    }

    void OnScoreValueChange(int score) {
        scoreText.text = score.ToString();
    }

}
