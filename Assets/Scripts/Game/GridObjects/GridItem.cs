using Assets.Scripts.Game.Grid;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    /// <summary>
    /// An inactive grid item is something that has a place on the grid and can be interacted with, but does not actively move or interact itself
    /// </summary>
    public abstract class GridItem : GridItemBase
    {
        [SerializeField]
        private float animTimeScale = 0.98f;

        public void Initialize(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            GridManager.Instance.RegisterGridItem(this);
            UpdateWorldPosition(false);
        }

        protected void UpdateWorldPosition(bool animate)
        {
            if (animate)
            {
                AnimateUpdatePosition();
            }
            else
            {
                transform.position = new Vector3(Coordinates.x, 0, Coordinates.y);
            }
        }

        private void AnimateUpdatePosition()
        {
            transform.DOMove(new Vector3(Coordinates.x, 0, Coordinates.y), GameManager.Instance.TimeBetweenHeartbeats * animTimeScale);
        }
    }
}