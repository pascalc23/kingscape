using Assets.Scripts.Game.Grid;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    /// <summary>
    /// An inactive grid item is something that has a place on the grid and can be interacted with, but does not actively move or interact itself
    /// </summary>
    public abstract class GridItem : GridItemBase
    {
        public void Initialize(Vector2Int coordinates)
        {
            SetCoordinates(coordinates, false);
            GridManager.Instance.RegisterGridItem(this);
        }

        public void DestroySelf()
        {
            // Remove us from the grid and animate destruction
            gridManager.RemoveGridItem(this);
            AnimateDestroy();
        }

        private void AnimateDestroy()
        {
            Destroy(gameObject);
        }
    }
}