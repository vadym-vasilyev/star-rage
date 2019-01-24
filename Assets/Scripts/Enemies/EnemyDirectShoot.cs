using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectShoot : AbstractRandomTimeEnemyShoot {
    private PlayerShip playerShip;

    protected override void DoShoot() {
        if (playerShip) {
            var dir = playerShip.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            var projectle = Instantiate(projectilePrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward), projectileParent.transform);
            projectle.GetComponent<Rigidbody2D>().velocity = projectle.transform.up * projectileSpeed;
            audioSource.PlayOneShot(shootSound);
        }
    }

    protected override void Start() {
        playerShip = FindObjectOfType<PlayerShip>();
        base.Start();
    }
}
