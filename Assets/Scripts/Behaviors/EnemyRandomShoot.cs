using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomShoot : MonoBehaviour {

    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] float minTimeBeforShoots = 0.4f;
    [SerializeField] float maxTimeBeforShoots = 2f;
    [SerializeField] float projectileSpeed = 6f;

    private bool shootScheduled = false;
    private GameObject projectileParent;

    void Start() {
        if (!projectilePrefab) {
            Debug.LogError("Projectile should be setted for this component");
        }
        projectileParent = GameObject.Find(PredefinedStrings.PROJECTILE_PARENT_OBJECT);
        if (!projectileParent) {
            projectileParent = new GameObject(PredefinedStrings.PROJECTILE_PARENT_OBJECT);
        }
    }

    //TODO: maybe better do not use coroutine everywere? Need to be checked
    void Update() {
        if (!shootScheduled) {
            StartCoroutine(ScheduleShoot());
        }
    }

    private IEnumerator ScheduleShoot() {
        shootScheduled = true;
        yield return new WaitForSeconds(Random.Range(minTimeBeforShoots, maxTimeBeforShoots));
        var projectle = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectileParent.transform);
        projectle.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
        shootScheduled = false;
    }
}
