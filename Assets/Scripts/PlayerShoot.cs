using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] float fireRate = 1f;

    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] float projectileSpeed = 10f;

    private GameObject projectileParent;
    private bool shootCoroutineRunning;

    // Start is called before the first frame update
    void Start() {
        projectileParent = GameObject.Find(PredefinedStrings.PROJECTILE_PARENT_OBJECT);
        if (!projectileParent) {
            projectileParent = new GameObject(PredefinedStrings.PROJECTILE_PARENT_OBJECT);
        }
    }

    // Update is called once per frame
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
            yield return new WaitForSeconds(fireRate);
        } while (CrossPlatformInputManager.GetButton("Fire1"));
        shootCoroutineRunning = false;
    }
}
