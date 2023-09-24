using Common;
using Game.Grid;
using UnityEngine;

namespace Game.GridObjects
{
    public class GridItemSpawner : Singleton<GridItemSpawner>
    {
        [SerializeField] private Transform container;

        private GridManager _gridManager;
        private GridItem[] _gridItemPrefabs;

        private void Start()
        {
            _gridManager = GridManager.Instance;
            GameManager.Instance.eventAfterHeartbeat.AddListener(AfterHeartbeat);
        }

        public void SetItemsToSpawn(GridItem[] gridItemPrefabs)
        {
            _gridItemPrefabs = gridItemPrefabs;
        }

        private void AfterHeartbeat(int heartbeat)
        {
            if (heartbeat >= _gridItemPrefabs.Length) return;
            GridItem itemPrefab = _gridItemPrefabs[heartbeat];
            if (itemPrefab != null)
            {
                SpawnItem(itemPrefab);
            }
        }

        private void SpawnItem(GridItem itemPrefab)
        {
            GridItem gridItem = Instantiate(itemPrefab, container);
            gridItem.Initialize(_gridManager.levelStartTile.Coordinates);
        }
    }
}