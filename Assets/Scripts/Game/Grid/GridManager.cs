using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Grid.Tiles;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class GridManager : Singleton<GridManager>
    {
        private Dictionary<Vector2Int, Tile> _tiles = new();

        public void RegisterTile(Vector2Int coordinates, Tile tile)
        {
            if (_tiles[coordinates] != null) throw new Exception($"Cannot register tile at coordinate {coordinates} - Another tile is already registered there");
            _tiles[coordinates] = tile;
        }
    }
}