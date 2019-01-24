using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheidView : MonoBehaviour {
    private ProgressBar progressBar;
    private PlayerStatController playerStatController;

    void Start() {
        playerStatController = FindObjectOfType<PlayerStatController>();
        progressBar = GetComponent<ProgressBar>();
        playerStatController.OnShieldValueChange += OnSheildValueChange;
        progressBar.BarValue = playerStatController.GetShield;
    }

    void OnSheildValueChange(int shield) {
        progressBar.BarValue = shield;
    }
}
