using MovementStrategy;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movementSpeed = 1f;

    private IMovementStrategy movementStrategy = null;

    public void ApplyMovementStrategy(IMovementStrategy movementStrategy) {
        this.movementStrategy = movementStrategy;
        movementStrategy.InitStartPosition(gameObject);
    }

    void Update() {
        bool isMovingFinished = movementStrategy.Move(gameObject, movementSpeed * Time.deltaTime);
        if (isMovingFinished) {
            Destroy(gameObject);
        }
    }
}