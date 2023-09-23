using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public abstract class MovingGridItem : ActiveGridItem
    {
        [SerializeField] private Vector2Int forwardVector;

        public void ChangeDirection(Vector2Int direction)
        {
            forwardVector = direction;
        }

        protected override void OnHeartbeat(int heartbeat)
        {
            if (isHalted) return;
            Interact(forwardVector); // Try to interact first
            Move(forwardVector);
        }

        protected override void OnHalt()
        {
            base.OnHalt();
            Debug.Log($"'{name}' halted at {Coordinates}");
        }
    }
}