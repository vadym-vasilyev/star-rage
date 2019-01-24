using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectShoot : AbstractRandomTimeEnemyShoot {
    private PlayerShip playerShip;

    protected override void DoShoot() {
        if (playerShip) {
            var projectle = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectileParent.transform);
            projectle.transform.LookAt(playerShip.transform);
            Debug.Log("Toward vector " + transform.forward + " (magnitude " + transform.forward.magnitude + ") normalized");
            projectle.GetComponent<Rigidbody2D>().AddForce(transform.forward.normalized * projectileSpeed);
            audioSource.PlayOneShot(shootSound);
        }
    }

    protected override void Start() {
        playerShip = FindObjectOfType<PlayerShip>();
        base.Start();
    }
}
