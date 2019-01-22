using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOwner : MonoBehaviour {

    [SerializeField] AudioClip deathSound = null;
    [SerializeField] ParticleSystem deathFX = null;
    [SerializeField] int health = 1;

    public void DecreaseHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            DoDeath();
        }
    }

    private void DoDeath() {
        if (deathSound) {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        }
        if (deathFX) {
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
