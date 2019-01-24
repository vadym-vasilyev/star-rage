using MovementStrategy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour {

    [SerializeField] float spawnXPadding = 0.3f;
    [SerializeField] WaveConfig[] waveConfigs = null;
    
    private GameObject enemiesParent;
    private MovementStrategyFactory movementStrategyFactory;

    void Start() {

        Camera gameCamera = Camera.main;
        float xSpawnMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spawnXPadding;
        float xSpawnMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spawnXPadding;

        float ySpawnPos = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        movementStrategyFactory = new MovementStrategyFactory(xSpawnMin, xSpawnMax, ySpawnPos);

        enemiesParent = GameObject.Find(PredefinedStrings.PARENT_OBJECT_ENEMY);
        if (!enemiesParent) {
            enemiesParent = new GameObject(PredefinedStrings.PARENT_OBJECT_ENEMY);
        }
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves() {
        foreach (WaveConfig waveConfig in waveConfigs) {
            StartCoroutine(StartSpawningEnemiesInWave(waveConfig));
            yield return new WaitForSeconds(waveConfig.timeToNextWave);
        }
    }

    //TODO: think about enemies pool instead instantiation each time
    private IEnumerator StartSpawningEnemiesInWave(WaveConfig waveConfig) {
        for (int i = 0; i < waveConfig.numberOfEnemies; i++) {
            var enemy = Instantiate(waveConfig.enemyPrefab, enemiesParent.transform);
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.ApplyMovementStrategy(movementStrategyFactory.createMovementStrategy(waveConfig.movementStrategyData), waveConfig.moveSpeed);
            yield return new WaitForSeconds(waveConfig.timeBetweenSpawns);
        }
    }

}
