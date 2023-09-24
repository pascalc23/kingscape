using UnityEngine;

namespace Game.GridObjects
{
    /// <summary>
    /// An active grid item is something that has a place on the grid and is actively participating (e.g. doing something on heartbeat)
    /// </summary>
    public abstract class ActiveGridItem : GridItem
    {
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

        protected virtual void OnInteract(IInteractable interactable)
        {
        }
    }
}