using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] float fireRate = 1f;

    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] AudioClip shootSound = null;
    [SerializeField] float projectileSpeed = 10f;


    private GameObject projectileParent;
    private bool shootCoroutineRunning;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        projectileParent = GameObject.Find(PredefinedStrings.PARENT_OBJECT_PROJECTILE);
        if (!projectileParent) {
            projectileParent = new GameObject(PredefinedStrings.PARENT_OBJECT_PROJECTILE);
        }
    }

    void Update() {
        Shoot();
    }

    //TODO: Move all shoot stuff to separate component "Shooter"?
    private void Shoot() {
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && !shootCoroutineRunning) {
            StartCoroutine(ShootContinuously());
        }
    }

    private IEnumerator ShootContinuously() {
        shootCoroutineRunning = true;
        do {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectileParent.transform);
            projectile.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed;
            audioSource.PlayOneShot(shootSound);
            yield return new WaitForSeconds(fireRate);
        } while (CrossPlatformInputManager.GetButton("Fire1"));
        shootCoroutineRunning = false;
    }
}
