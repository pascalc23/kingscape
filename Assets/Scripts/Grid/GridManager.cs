using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Vector2 gridSize;
        [SerializeField] private Transform gridContainer;
        [SerializeField] private Tile tilePrefab;

        private Tile[][] _grid;

        private void Start()
        {
            SpawnGrid();
        }

        private void SpawnGrid()
        {
            int shiftX = Mathf.RoundToInt(gridSize.x / 2f);
            int shiftY = Mathf.RoundToInt(gridSize.y / 2f);
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Tile tile = Instantiate(tilePrefab, gridContainer);
                    tile.Initialize(new Vector2(x - shiftX, y - shiftY));
                }
            }
        }
    }
}