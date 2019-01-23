using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOwner : MonoBehaviour {

    [SerializeField] AudioClip deathSound = null;
    [SerializeField] ParticleSystem deathFX = null;
    [SerializeField] int health = 1;
    [SerializeField] int score = 30;

    private PlayerStatController playerStatController;

    void Start() {
        playerStatController = FindObjectOfType<PlayerStatController>();
    }

    public void DecreaseHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            DoDeath();
        }
    }

    private void DoDeath() {
        playerStatController.IncreaseScore(score);
        if (deathSound) {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        }
        if (deathFX) {
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
