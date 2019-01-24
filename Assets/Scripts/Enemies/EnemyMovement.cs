using MovementStrategy;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour {

    private float movementSpeed;
    private IMovementStrategy movementStrategy = null;

    public void ApplyMovementStrategy(IMovementStrategy movementStrategy, float movementSpeed) {
        this.movementStrategy = movementStrategy;
        this.movementSpeed = movementSpeed;
        movementStrategy.InitStartPosition(gameObject);
    }

    void Update() {
        if (movementStrategy == null) {
            Debug.LogWarning("This behavior should be provided with movement strategy!");
            return;
        }
        bool isMovingFinished = movementStrategy.Move(gameObject, movementSpeed * Time.deltaTime);
        if (isMovingFinished) {
            Destroy(gameObject);
        }
    }
}