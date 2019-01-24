using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthOwner))]
public class EnemyShip : MonoBehaviour {

    [SerializeField] AudioClip deathSound = null;
    [SerializeField] ParticleSystem deathFX = null;
    [SerializeField] ParticleSystem hitFX = null;
    [SerializeField] int scoreForKilling = 30;
    [SerializeField] GameObject audioPlayer = null;
    

    private PlayerStatController playerStatController;
    private GameObject particlesParentObject;

    protected HealthOwner healthOwner;

    protected virtual void Start() {
        particlesParentObject = GameObject.Find(PredefinedStrings.PARENT_PARTICLES);
        if (!particlesParentObject) {
            particlesParentObject = new GameObject(PredefinedStrings.PARENT_PARTICLES);
        }

        playerStatController = FindObjectOfType<PlayerStatController>();
        healthOwner = GetComponent<HealthOwner>();
        healthOwner.OnHealthEnded += DoDeath;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        var damangeDealer = collision.gameObject.GetComponent<DamangeDealer>();
        if (damangeDealer) {
            healthOwner.DecreaseHealth(damangeDealer.DamangeAmount);
            Destroy(collision.gameObject);
            if (hitFX) {
                Instantiate(hitFX, transform.position, Quaternion.identity, particlesParentObject.transform);
            }
        }
    }

    //TODO: Find one dedicated aoudio source from scene instead insatantinate
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
            Instantiate(deathFX, transform.position, Quaternion.identity, particlesParentObject.transform);
        }
        Destroy(gameObject);
    }
}
