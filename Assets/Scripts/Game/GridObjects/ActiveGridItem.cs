using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
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

        protected void Move(Vector2Int direction)
        {
            Vector2Int destination = Coordinates + direction;
            if (gridManager.CanMove(this, destination))
            {
                gridManager.Move(this, destination);
                UpdateWorldPosition();
            }
        }

        public void AnimateDestroy()
        {
            Destroy(gameObject);
        }
    }
}