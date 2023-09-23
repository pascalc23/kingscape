using Assets.Scripts.Common;
using Assets.Scripts.Game.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class GridItemSpawner : Singleton<GridItemSpawner>
    {
        [SerializeField] private Transform container;

        private GridManager _gridManager;
        private GridItem[] _gridItemPrefabs;

        private List<GridItem> _spawnedItems = new List<GridItem>();

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
            _spawnedItems.Add(gridItem);
        }

        public void ResetSpawnedItems()
        {
            foreach (var item in _spawnedItems)
            {
                item.DestroySelf();
            }
            _spawnedItems.Clear();
        }
    }
}