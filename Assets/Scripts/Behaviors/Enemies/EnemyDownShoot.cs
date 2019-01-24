using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDownShoot : AbstractRandomTimeEnemyShoot {
    protected override void DoShoot() {
        var projectle = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectileParent.transform);
        projectle.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
        audioSource.PlayOneShot(shootSound);
    }
}
