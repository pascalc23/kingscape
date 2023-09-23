using Assets.Scripts.Grid.Tiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField]
    private Tile basicTilePrefab;
    [SerializeField]
    private Vector2Int gridSize;
    [SerializeField]
    private int tileSize;

    List<Tile> tiles = new List<Tile>();

    [ContextMenu("SpawnGridInEditor")]
    private void SpawnGridInEditor()
    {
        RemoveGridInEditor();

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var go = Instantiate(basicTilePrefab, transform);
                go.transform.position = new Vector3(i * tileSize, 0, j * tileSize);
                go.UpdateName();
                tiles.Add(go);
            }
        }
    }

    [ContextMenu("RemoveGridInEditor")]
    private void RemoveGridInEditor()
    {
        foreach (var item in tiles)
        {
            DestroyImmediate(item.gameObject);
        }
        tiles.Clear();
    }
}
