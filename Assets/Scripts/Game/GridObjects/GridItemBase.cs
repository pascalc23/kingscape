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

        public void SetCoordinates(Vector2Int value)
        {
            Coordinates = value;
        }

        public void ChangeCoordinates(Vector2Int destination, bool animateUpdate = true)
        {
            Vector2Int delta = destination - Coordinates;
            SetCoordinates(destination);
            ChangeWorldPosition(delta, animateUpdate);
        }

        private void ChangeWorldPosition(Vector2Int deltaCoordinates, bool animate)
        {
            Vector3 deltaPosition = new Vector3(deltaCoordinates.x, 0, deltaCoordinates.y);
            if (animate)
            {
                AnimateUpdatePosition(deltaPosition);
            }
            else
            {
                transform.localPosition += deltaPosition;
            }
        }

        private void AnimateUpdatePosition(Vector3 deltaPosition)
        {
            transform.DOMove(transform.localPosition + deltaPosition, GameManager.Instance.TimeBetweenHeartbeats * animTimeScale);
        }

        protected Vector2Int GetCoordinatesFromWorldPosition()
        {
            var position = transform.localPosition;
            return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
        }
    }
}