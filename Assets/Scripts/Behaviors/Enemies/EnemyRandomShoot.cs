using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: add more shoot behaviors for enemy
public class EnemyRandomShoot : MonoBehaviour {

    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] AudioClip shootSound = null;
    [SerializeField] float minTimeBeforShoots = 0.4f;
    [SerializeField] float maxTimeBeforShoots = 2f;
    [SerializeField] float projectileSpeed = 6f;

    private bool shootScheduled = false;
    private AudioSource audioSource;
    private GameObject projectileParent;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        projectileParent = GameObject.Find(PredefinedStrings.PARENT_OBJECT_PROJECTILE);
        if (!projectileParent) {
            projectileParent = new GameObject(PredefinedStrings.PARENT_OBJECT_PROJECTILE);
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
        audioSource.PlayOneShot(shootSound);
        shootScheduled = false;
    }
}
