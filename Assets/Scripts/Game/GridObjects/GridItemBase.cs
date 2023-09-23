using Assets.Scripts.Game.Grid;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    /// <summary>
    /// A grid item is something that has a place (coordinates) on the grid.  
    /// </summary>
    public abstract class GridItemBase : MonoBehaviour
    {
        [SerializeField]
        private float animTimeScale = 0.98f;

        public Vector2Int Coordinates { get; set; }

        protected GridManager gridManager;

        private void Start()
        {
            gridManager = GridManager.Instance;
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void UpdateWorldPosition(bool animate)
        {
            if (animate)
            {
                transform.DOMove(new Vector3(Coordinates.x, 0, Coordinates.y), GameManager.Instance.TimeBetweenHeartbeats * animTimeScale);
            }
            else
            {
                transform.position = new Vector3(Coordinates.x, 0, Coordinates.y);
            }
        }

        protected Vector2Int GetCoordinatesFromWorldPosition()
        {
            var position = transform.position;
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        }
    }
}