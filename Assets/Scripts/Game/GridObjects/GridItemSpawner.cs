using Assets.Scripts.Common;
using Assets.Scripts.Game.Grid;
using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class GridItemSpawner : Singleton<GridItemSpawner>
    {
        [SerializeField] private Transform container;
        [SerializeField] private GridItem[] gridItems;

        private GridManager _gridManager;

        private void Start()
        {
            _gridManager = GridManager.Instance;
            GameManager.Instance.eventAfterHeartbeat.AddListener(AfterHeartbeat);
        }

        public void SetItemsToSpawn(GridItem[] gridItemPrefabs)
        {
            gridItems = gridItemPrefabs;
        }

        private void AfterHeartbeat(int heartbeat)
        {
            if (heartbeat >= gridItems.Length) return;
            GridItem itemPrefab = gridItems[heartbeat];
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