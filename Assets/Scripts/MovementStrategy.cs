using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO: Breake for separate classe;
namespace MovementStrategy {

    [System.Serializable]
    public class MovementStrategyData {
        public MovementStrategyType movementStrategy = MovementStrategyType.MoveDown;

        [Tooltip("For movement strategy Move By Waypoint")]
        public Transform path;
        [Tooltip("For movement strategy Move By Random point")]
        public float minDistance = 0f;
        [Tooltip("For movement strategy Move By Random point")]
        public Rect bound;
    }

    public enum MovementStrategyType {
        MoveByWaypoint,
        MoveDown,
        MoveByRandomPoint
    }

    public interface IMovementStrategy {
        bool Move(GameObject objectToMove, float maxMoveStep);
        void InitStartPosition(GameObject objectToMove);
    }

    public class MovementStrategyFactory {
        private float xSpawnMin, xSpawnMax;
        private float ySpawnPos;

        public MovementStrategyFactory(float xSpawnMin, float xSpawnMax, float ySpawnPos) {
            this.xSpawnMin = xSpawnMin;
            this.xSpawnMax = xSpawnMax;
            this.ySpawnPos = ySpawnPos;
        }

        public IMovementStrategy createMovementStrategy(MovementStrategyData strategyData) {
            switch (strategyData.movementStrategy) {
                case MovementStrategyType.MoveByRandomPoint:
                    return new MovementByRandomPointStrategy(strategyData.bound, generateStartPosition(), strategyData.minDistance);
                case MovementStrategyType.MoveByWaypoint:
                    return new MovementByWaypointsStrategy(PathToWaypoints(strategyData.path));
                case MovementStrategyType.MoveDown:
                    return new MovementDownStrategy(generateStartPosition());
                default:
                    Debug.LogError("Oops! Unimplemented strategy type: " + strategyData.movementStrategy);
                    return null;
            }
        }

        private Vector2 generateStartPosition() {
            return new  Vector2(Random.Range(xSpawnMin, xSpawnMax), ySpawnPos);
        }

        private Transform[] PathToWaypoints(Transform path) {
            return path.Cast<Transform>().ToArray();
        }
    }

    class MovementByWaypointsStrategy : IMovementStrategy {
        private Transform[] waypoints;
        private int waypointIndex = 1;

        public MovementByWaypointsStrategy(Transform[] waypoints) {
            this.waypoints = waypoints;
        }

        public void InitStartPosition(GameObject objectToMove) {
            objectToMove.transform.position = waypoints[0].position;
        }

        public bool Move(GameObject objectToMove, float maxMoveStep) {
            if (waypointIndex < waypoints.Length) {
                Transform waypoint = waypoints[waypointIndex];
                Vector2 newStepForWaypoint = Vector2.MoveTowards(objectToMove.transform.position, waypoint.position, maxMoveStep);
                objectToMove.transform.position = newStepForWaypoint;
                if (objectToMove.transform.position == waypoint.position) {
                    waypointIndex++;
                }
                return false;
            }
            return true;
        }
    }

    class MovementDownStrategy : PredefinedStartPositionMovement {

        public MovementDownStrategy(Vector2 startPos) : base(startPos) {}

        public override bool Move(GameObject objectToMove, float maxMoveStep) {
            objectToMove.transform.Translate(Vector3.down * maxMoveStep);
            return false;
        }
    }

    class MovementByRandomPointStrategy : PredefinedStartPositionMovement {

        private Vector3 nextPosition;
        private float minDistance;
        Rect movementBounds;


        public MovementByRandomPointStrategy(Rect movementBounds, Vector2 startPos, float ybound, float minDistance = 0f) : base(startPos) {
            this.minDistance = minDistance;
            this.movementBounds = movementBounds;
            CheckIsMinDistanceIsNoToBig();
            SelectNextPosition();
        }


        public override bool Move(GameObject objectToMove, float maxMoveStep) {
            Vector2 newStepForPoint = Vector2.MoveTowards(objectToMove.transform.position, nextPosition, maxMoveStep);
            objectToMove.transform.position = newStepForPoint;
            if (objectToMove.transform.position == nextPosition) {
                SelectNextPosition();
            }
            return false;
        }

        private void SelectNextPosition() {
            do {
                float XPos = Random.Range(movementBounds.xMin, movementBounds.xMax);
                float YPos = Random.Range(movementBounds.yMin, movementBounds.yMax);
                nextPosition = new Vector3(XPos, YPos);
            } while (nextPosition.magnitude < minDistance);

        }

        private void CheckIsMinDistanceIsNoToBig() {
            Vector2 centerOfBound = new Vector2((movementBounds.xMax - movementBounds.xMin) / 2, (movementBounds.yMax - movementBounds.yMin) / 2);

            if (centerOfBound.magnitude < minDistance) {
                Debug.LogError("To big minDistance value, possile case when we can't find nextPosition > minDistance");
            }
        }
    }

    abstract class PredefinedStartPositionMovement: IMovementStrategy {
        protected Vector2 startPos;

        protected PredefinedStartPositionMovement(Vector2 startPos) {
            this.startPos = startPos;
        }

        public void InitStartPosition(GameObject objectToMove) {
            float height = objectToMove.GetComponent<SpriteRenderer>().bounds.size.y;
            objectToMove.transform.position = new Vector2(startPos.x, startPos.y + height);
        }

        public abstract bool Move(GameObject objectToMove, float maxMoveStep);
    }
}
