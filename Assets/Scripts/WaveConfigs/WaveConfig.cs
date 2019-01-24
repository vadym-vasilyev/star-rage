using MovementStrategy;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Wave configuration")]
    public EnemyShip enemyPrefab;
    public float timeBetweenSpawns = 0.5f;
    public float timeToNextWave = 0.3f;
    public int numberOfEnemies = 5;
    public float moveSpeed = 2f;

    [Header("Data for movement strategy")]
    public MovementStrategyData movementStrategyData;

}
