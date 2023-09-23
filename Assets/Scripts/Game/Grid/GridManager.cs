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
        private Dictionary<Vector2Int, ActiveGridItem> _gridItems = new();

        public void RegisterTile(Tile tile)
        {
            if (_tiles.ContainsKey(tile.Coordinates)) throw new Exception($"Cannot register tile at coordinate {tile.Coordinates} - Another tile is already occupying that space");
            _tiles[tile.Coordinates] = tile;
            Debug.Log($"[{GetType().Name}] Registered tile '{tile.name}' at coordinates {tile.Coordinates}");
        }

        public void RegisterGridItem(ActiveGridItem gridItem)
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
        /// Returns true if the given <paramref name="activeGridItem"/> can move to the target position
        /// </summary>
        public bool CanMove(ActiveGridItem activeGridItem, Vector2Int destination)
        {
            // If the target tile does not exist we cannot move there
            Tile targetTile = _tiles[destination];
            if (targetTile == null) return false;

            // If the target tile is impassable we cannot move there
            if (targetTile.Type.isWalkable == false) return false;

            // If the destination is already occupied we cannot move there
            return !IsOccupied(destination);
        }

        public void Move(ActiveGridItem activeGridItem, Vector2Int destination)
        {
            // Ensure we actually are still located at the current coordinates
            Assert.IsTrue(_gridItems[activeGridItem.Coordinates] == activeGridItem);

            // If the destination is already occupied we cannot move there
            if (!CanMove(activeGridItem, destination))
            {
                throw new Exception($"[{GetType().Name}] I tried to move item '{activeGridItem.name}' to grid location {destination} but I cannot move there");
            }

            // Move the piece
            _gridItems.Remove(activeGridItem.Coordinates);
            _gridItems[destination] = activeGridItem;
            activeGridItem.Coordinates = destination;
        }

        public bool IsOccupied(Vector2Int coordinates)
        {
            return _gridItems.ContainsKey(coordinates);
        }
    }
}