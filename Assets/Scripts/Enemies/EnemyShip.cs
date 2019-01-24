using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthOwner))]
public class EnemyShip : MonoBehaviour {

    [SerializeField] AudioClip deathSound = null;
    [SerializeField] ParticleSystem deathFX = null;
    [SerializeField] int scoreForKilling = 30;
    [SerializeField] GameObject audioPlayer;

    private PlayerStatController playerStatController;

    protected HealthOwner healthOwner;

    public void OnTriggerEnter2D(Collider2D collision) {
        var damangeDealer = collision.gameObject.GetComponent<DamangeDealer>();
        if (damangeDealer) {
            healthOwner.DecreaseHealth(damangeDealer.DamangeAmount);
            Destroy(collision.gameObject);
        }
    }

    protected virtual void Start() {
        playerStatController = FindObjectOfType<PlayerStatController>();
        healthOwner = GetComponent<HealthOwner>();
        healthOwner.OnHealthEnded += DoDeath;
    }

    private void DoDeath() {
        playerStatController.IncreaseScore(scoreForKilling);
        if (deathSound && audioPlayer) {
            var audioPlayerInstance = Instantiate(audioPlayer);
            AudioSource audioSource = audioPlayerInstance.GetComponent<AudioSource>();
            audioSource.clip = deathSound;
            audioSource.Play();
            Destroy(audioPlayerInstance, deathSound.length);

        }
        if (deathFX) {
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
