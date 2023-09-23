using DG.Tweening;
using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public abstract class MovingGridItem : ActiveGridItem
    {
        [SerializeField] protected Vector2Int forwardVector;
        [SerializeField] private GameObject model;

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