using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AbstractRandomTimeEnemyShoot : MonoBehaviour {
    [SerializeField] protected GameObject projectilePrefab = null;
    [SerializeField] protected AudioClip shootSound = null;
    [SerializeField] protected float projectileSpeed = 6f;

    protected AudioSource audioSource;
    protected GameObject projectileParent;

    [SerializeField] float minTimeBeforShoots = 0.4f;
    [SerializeField] float maxTimeBeforShoots = 2f;

    private bool shootScheduled = false;

    protected virtual void Start() {
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
        DoShoot();
        shootScheduled = false;
    }

    protected abstract void DoShoot();
}
