using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Game.Grid.Tiles;
using Assets.Scripts.Game.GridObjects;
using Assets.Scripts.Game.GridObjects.Obstacles;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Game.Grid
{
    public class GridManager : Singleton<GridManager>
    {
        public Tile levelStartTile;
        public Tile levelFinishTile;

        private Dictionary<Vector2Int, Tile> _tiles = new();
        private Dictionary<Vector2Int, GridItem> _gridItems = new();

        public void ResetLevel()
        {
            foreach (GridItem gridItem in _gridItems.Values)
            {
                Destroy(gridItem.gameObject);
            }

            _gridItems.Clear();
        }

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
            MoveItem(activeGridItem, destination);

            // Check if the item is on the finish tile
            if (IsFinishTile(destination))
            {
                activeGridItem.OnFinish();
            }
        }

        /// <summary>
        /// Returns true if the given <paramref name="destination"/> coordinates contain an interactable grid item
        /// </summary>
        public bool CanInteract(Vector2Int destination)
        {
            _gridItems.TryGetValue(destination, out GridItem targetItem);
            return targetItem != null && targetItem is IInteractable;
        }

        private bool IsFinishTile(Vector2Int destination)
        {
            return levelFinishTile == _tiles[destination];
        }

        public bool IsOccupied(Vector2Int coordinates)
        {
            return _gridItems.ContainsKey(coordinates);
        }

        public GridItem GetGridItem(Vector2Int coordinates)
        {
            return _gridItems.TryGetValue(coordinates, out var item) ? item : null;
        }

        /// <summary>
        /// Removes an item from the grid
        /// </summary>
        /// <param name="gridItem"></param>
        public void RemoveGridItem(GridItem gridItem)
        {
            if (_gridItems.ContainsKey(gridItem.Coordinates))
            {
                _gridItems.Remove(gridItem.Coordinates);
            }
        }

        /// <summary>
        /// Tries to push the item from the given <paramref name="coordinates"/> to the given <paramref name="direction"/>.
        /// Returns true if it did push something. 
        /// </summary>
        public bool TryPush(GridItem pusher, Vector2Int coordinates, Vector2Int direction)
        {
            // Ensure there is an obstacle at the provided coordinates
            GridItem gridItem = GetGridItem(coordinates);
            if (gridItem == null || gridItem is not Obstacle) return false;

            // Ensure the obstacle is movable
            Obstacle obstacle = (Obstacle)gridItem;
            if (!obstacle.Type.isMovable) return false;

            // Ensure there is nothing placed at the tile "behind" the provided coordinates
            if (GetGridItem(coordinates + direction) != null) return false;

            // If all checks out we can push the item forward
            MoveItem(obstacle, coordinates + direction);
            MoveItem(pusher, coordinates);

            return true;
        }

        private void MoveItem(GridItem gridItem, Vector2Int destination)
        {
            // Remove the piece from the current position
            _gridItems.Remove(gridItem.Coordinates);

            // Update coordinates and grid position
            gridItem.SetCoordinates(destination);
            _gridItems[destination] = gridItem;
            Debug.Log($"[{GetType().Name}] Moved item '{gridItem.name}' to coordinates {destination}");
        }
    }
}