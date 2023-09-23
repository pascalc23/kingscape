using Assets.Scripts.Game.Grid;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    /// <summary>
    /// A grid item is something that has a place (coordinates) on the grid.  
    /// </summary>
    public abstract class GridItem : MonoBehaviour
    {
        public Vector2Int Coordinates { get; set; }

        protected GridManager gridManager;

        private void Awake()
        {
            Coordinates = GetCoordinatesFromWorldPosition();
        }

        private void Start()
        {
            gridManager = GridManager.Instance;
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnValidate()
        {
            UpdateCoordinates();
        }

        protected void UpdateCoordinates()
        {
            Coordinates = GetCoordinatesFromWorldPosition();
        }

        protected void UpdateWorldPosition()
        {
            transform.position = new Vector3(Coordinates.x, 0, Coordinates.y);
        }

        private Vector2Int GetCoordinatesFromWorldPosition()
        {
            var position = transform.position;
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        }
    }
}