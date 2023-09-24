using DG.Tweening;
using Game.Grid;
using UnityEngine;

namespace Game.GridObjects
{
    /// <summary>
    /// A grid item is something that has a place (coordinates) on the grid.  
    /// </summary>
    public abstract class GridItemBase : MonoBehaviour
    {
        [SerializeField] private float animTimeScale = 0.98f;
        public Vector2Int Coordinates { get; private set; }

        protected GridManager gridManager;

        private void Start()
        {
            gridManager = GridManager.Instance;
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        public void SetCoordinates(Vector2Int value, bool animateUpdate = true)
        {
            Coordinates = value;
            UpdateWorldPosition(animateUpdate);
        }
        
        private void UpdateWorldPosition(bool animate)
        {
            if (animate)
            {
                AnimateUpdatePosition();
            }
            else
            {
                transform.position = new Vector3(Coordinates.x, transform.position.y, Coordinates.y);
            }
        }

        private void AnimateUpdatePosition()
        {
            transform.DOMove(new Vector3(Coordinates.x, transform.position.y, Coordinates.y), GameManager.Instance.TimeBetweenHeartbeats * animTimeScale);
        }

        protected Vector2Int GetCoordinatesFromWorldPosition()
        {
            var position = transform.position;
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        }
    }
}