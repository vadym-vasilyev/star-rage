using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour {

    [SerializeField] float shipSpeed = 1f;
    [SerializeField] float padding = 1f;
    [Range(0, 40)] [SerializeField] float rollMaxAngel = 20f;
    [SerializeField] float fireRate = 1f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;

    private GameObject projectileParent;
    private bool shootCoroutineRunning;

    private float xMin, xMax;
    private float yMin, yMax;

    void Start() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

        projectileParent = GameObject.Find("ProjectileParent");
        if (!projectileParent) {
            projectileParent = new GameObject("ProjectileParent");
        }
    }

    void Update() {
        MoveShip();
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
            yield return new WaitForSeconds(fireRate);
        } while (CrossPlatformInputManager.GetButton("Fire1"));
        shootCoroutineRunning = false;
    }

    private void MoveShip() {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");

        float deltaX = inputX * Time.deltaTime * shipSpeed;
        float deltaY = inputY * Time.deltaTime * shipSpeed;

        float newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector3(newPosX, newPosY);

        float roll = rollMaxAngel * inputX;
        transform.rotation = Quaternion.Euler(0, roll, 0);
    }
}
