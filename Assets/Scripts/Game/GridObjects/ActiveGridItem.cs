using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    /// <summary>
    /// An active grid item is something that has a place on the grid and is actively participating (e.g. doing something on heartbeat)
    /// </summary>
    public abstract class ActiveGridItem : GridItem
    {
        protected bool isHalted;

        protected override void OnStart()
        {
            base.OnStart();
            GameManager.Instance.eventHeartbeat.AddListener(OnHeartbeat);
        }

        protected abstract void OnHeartbeat(int heartbeat);

        /// <summary>
        /// Tries to interact with the grid item ahead
        /// </summary>
        protected void Interact(Vector2Int direction)
        {
            Vector2Int destination = Coordinates + direction;
            if (gridManager.CanInteract(destination))
            {
                ((IInteractable)gridManager.GetGridItem(destination)).Interact(this);
            }
        }

        protected void Move(Vector2Int direction)
        {
            Vector2Int destination = Coordinates + direction;
            if (gridManager.CanMove(this, destination))
            {
                gridManager.Move(this, destination);
            }
            else
            {
                OnHalt();
            }
        }

        protected virtual void OnInteract(IInteractable interactable)
        {
        }

        /// <summary>
        /// Called when the active item is trying to move but can't
        /// </summary>
        protected virtual void OnHalt()
        {
            isHalted = true;
        }

        /// <summary>
        /// Called when the active item has reached the finish tile
        /// </summary>
        public virtual void OnFinish()
        {
            isHalted = true;
            gridManager.RemoveGridItem(this);
            AnimateDestroy();
        }

        protected void AnimateDestroy()
        {
            Destroy(gameObject);
        }
    }
}