using Audio;
using UnityEngine;

namespace Game.GridObjects
{
    public abstract class MovingGridItem : ActiveGridItem
    {
        [SerializeField] private GameObject model;

        protected Vector2Int forwardVector;

        public void Initialize(Vector2Int coordinates, Vector2Int forwardVector)
        {
            base.Initialize(coordinates);
            this.forwardVector = forwardVector;
            model.transform.right = new Vector3(-forwardVector.x, 0, -forwardVector.y);
        }

        public void ChangeDirection(Vector2Int direction)
        {
            forwardVector = direction;
            model.transform.right = new Vector3(-direction.x, 0, -direction.y);
            // TODO: animate
            //model.transform.DORotate(new Vector3(-direction.x, 0, -direction.y), 0.1f);
        }

        protected override void OnHeartbeat(int heartbeat)
        {
            if (isHalted) return;
            Interact(forwardVector); // Try to interact first
            Move(forwardVector, () => AudioManager.Instance.OnMove());
        }

        protected override void OnHalt()
        {
            base.OnHalt();
            Debug.Log($"'{name}' halted at {Coordinates}");
        }

        protected Vector2Int GetNextCoordinate()
        {
            return Coordinates + forwardVector;
        }
    }
}