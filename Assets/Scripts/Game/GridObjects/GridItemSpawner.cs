using Common;
using Game.Grid;
using Game.Levels;
using UnityEngine;

namespace Game.GridObjects
{
    public class GridItemSpawner : Singleton<GridItemSpawner>
    {
        [SerializeField] private Transform container;

        private GridManager _gridManager;
        private MovingGridItem[] _gridItemPrefabs;
        private Level _activeLevel;

        private void Start()
        {
            _gridManager = GridManager.Instance;
            GameManager.Instance.eventAfterHeartbeat.AddListener(AfterHeartbeat);
        }

        /// <summary>
        /// Loads a new level.
        /// </summary>
        public void LoadLevel(Level level)
        {
            _activeLevel = level;
        }

        public void SetItemsToSpawn(MovingGridItem[] gridItemPrefabs)
        {
            _gridItemPrefabs = gridItemPrefabs;
        }

        private void AfterHeartbeat(int heartbeat)
        {
            if (heartbeat >= _gridItemPrefabs.Length) return;
            MovingGridItem itemPrefab = _gridItemPrefabs[heartbeat];
            if (itemPrefab != null)
            {
                SpawnItem(itemPrefab);
            }
        }

        private void SpawnItem(MovingGridItem itemPrefab)
        {
            MovingGridItem gridItem = Instantiate(itemPrefab, container);
            gridItem.Initialize(_gridManager.levelStartTile.Coordinates, _activeLevel.startDirection);
        }
    }
}