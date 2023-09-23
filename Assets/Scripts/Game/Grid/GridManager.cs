using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Game.Grid.Tiles;
using Assets.Scripts.Game.GridObjects;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Game.Grid
{
    public class GridManager : Singleton<GridManager>
    {
        public Tile levelStartTile;
        public Tile levelFinishTile;
        public ActiveGridItem king;

        private Dictionary<Vector2Int, Tile> _tiles = new();
        private Dictionary<Vector2Int, GridItem> _gridItems = new();

        public void RegisterTile(Tile tile)
        {
            if (_tiles.ContainsKey(tile.Coordinates)) throw new Exception($"Cannot register tile at coordinate {tile.Coordinates} - Another tile is already occupying that space");
            _tiles[tile.Coordinates] = tile;
            Debug.Log($"[{GetType().Name}] Registered tile '{tile.name}' at coordinates {tile.Coordinates}");
        }

        public void RegisterGridItem(GridItem gridItem)
        {
            if (_gridItems.ContainsKey(gridItem.Coordinates))
                throw new Exception($"Cannot register grid item '{gridItem.name}' at coordinate {gridItem.Coordinates} - Another item is already occupying that space");
            _gridItems[gridItem.Coordinates] = gridItem;
            Debug.Log($"[{GetType().Name}] Registered item '{gridItem.name}' at coordinates {gridItem.Coordinates}");
        }

        public bool IsLevelReady()
        {
            return levelStartTile != null && levelFinishTile != null;
        }

        /// <summary>
        /// Returns true if the given <paramref name="activeGridItemBase"/> can move to the target position
        /// </summary>
        public bool CanMove(ActiveGridItem activeGridItemBase, Vector2Int destination)
        {
            // If the target tile does not exist we cannot move there
            if (!_tiles.TryGetValue(destination, out Tile targetTile)) return false;

            // If the target tile is impassable we cannot move there
            if (targetTile.Type.isWalkable == false) return false;

            // If the destination is already occupied we cannot move there
            return !IsOccupied(destination);
        }

        public void Move(ActiveGridItem activeGridItemBase, Vector2Int destination)
        {
            // Ensure we actually are still located at the current coordinates
            Assert.IsTrue(_gridItems[activeGridItemBase.Coordinates] == activeGridItemBase);

            // If the destination is already occupied we cannot move there
            if (!CanMove(activeGridItemBase, destination))
            {
                throw new Exception($"[{GetType().Name}] I tried to move item '{activeGridItemBase.name}' to grid location {destination} but I cannot move there");
            }

            // Remove the piece from the grid
            _gridItems.Remove(activeGridItemBase.Coordinates);

            // Move the piece
            activeGridItemBase.Coordinates = destination;

            // Put it back on the grid unless it reached the finish tile
            if (IsFinishTile(destination))
            {
                activeGridItemBase.AnimateDestroy();
            }
            else
            {
                _gridItems[destination] = activeGridItemBase;
            }
        }

        private bool IsFinishTile(Vector2Int destination)
        {
            return levelFinishTile == _tiles[destination];
        }

        public bool IsOccupied(Vector2Int coordinates)
        {
            return _gridItems.ContainsKey(coordinates);
        }
    }
}