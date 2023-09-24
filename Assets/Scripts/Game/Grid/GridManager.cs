using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Common;
using Game.Grid.Tiles;
using Game.GridObjects;
using Game.GridObjects.Obstacles;
using Game.Levels;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Grid
{
    public class GridManager : Singleton<GridManager>
    {
        public Tile levelStartTile;
        public Tile levelFinishTile;

        private Dictionary<Vector2Int, Tile> _tiles = new();
        private Dictionary<Vector2Int, GridItem> _gridItems = new();
        private Dictionary<Obstacle, Vector2Int> _initialObstacleCoordinates = new();

        /// <summary>
        /// Loads a new level.
        /// </summary>
        public void LoadLevel(Level level)
        {
            // Clear the old tiles and register the new level's tiles
            _tiles.Clear();
            RegisterTiles(GetTiles(level.gridContainer));

            // Set Start and End Tiles
            Debug.Log($"[{GetType().Name}] Setting {level.startTile.Coordinates} as start tile");
            levelStartTile = level.startTile;
            RegisterTile(levelStartTile);

            Debug.Log($"[{GetType().Name}] Setting {level.finishTile.Coordinates} as finish tile");
            levelFinishTile = level.finishTile;
            RegisterTile(levelFinishTile);

            // Reset everything else so the level is clear to play
            DestroyAllSpawnedItems(true);

            // Register all obstacles
            level.obstacles.ForEach(RegisterObstacle);
        }

        /// <summary>
        /// Resets the current level so the player can try again
        /// </summary>
        public void ResetLevel()
        {
            DestroyAllSpawnedItems(false);
            ResetObstaclePositions();
        }

        private void ResetObstaclePositions()
        {
            // Reset all obstacles to their original position
            foreach (Obstacle obstacle in _initialObstacleCoordinates.Keys)
            {
                obstacle.ChangeCoordinates(_initialObstacleCoordinates[obstacle]);
            }
        }

        private void DestroyAllSpawnedItems(bool includeObstacles)
        {
            // Destroy all items we spawned into the grid and remove them from the grid items
            List<GridItem> itemList = _gridItems.Values.ToList();
            List<GridItem> movingGridItems = includeObstacles ? itemList.FindAll(item => item is MovingGridItem or Obstacle) : itemList.FindAll(item => item is MovingGridItem);
            foreach (GridItem gridItem in movingGridItems)
            {
                _gridItems.Remove(gridItem.Coordinates);
                Destroy(gridItem.gameObject);
            }
        }

        private void RegisterObstacle(Obstacle obstacle)
        {
            RegisterGridItem(obstacle);
            _initialObstacleCoordinates[obstacle] = obstacle.Coordinates;
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
            if (!_tiles.TryGetValue(destination, out Tile targetTile))
            {
                Debug.Log($"[{GetType().Name}] Can't move to {destination} - target tile does not exist");
                return false;
            }

            // If the target tile is impassable we cannot move there
            if (targetTile.Type.isWalkable == false)
            {
                Debug.Log($"[{GetType().Name}] Can't move to {destination} - target tile of type '{targetTile.Type.title}' is not walkable");
                return false;
            }

            // If the destination is already occupied we cannot move there
            if (IsOccupied(destination))
            {
                GridItem gridItem = GetGridItem(destination);
                Debug.Log($"[{GetType().Name}] Can't move to {destination} - target tile of type '{targetTile.Type.title}' is occupied by item '{gridItem.name}'");
                return false;
            }

            return true;
        }

        public void Move(MovingGridItem gridItem, Vector2Int destination)
        {
            // Ensure we actually are still located at the current coordinates
            Assert.IsTrue(_gridItems[gridItem.Coordinates] == gridItem);

            // If the destination is already occupied we cannot move there
            if (!CanMove(gridItem, destination))
            {
                throw new Exception($"[{GetType().Name}] I tried to move item '{gridItem.name}' to grid location {destination} but I cannot move there");
            }

            // Move the piece
            MoveItem(gridItem, destination);

            // Check if the item is on the finish tile
            if (IsFinishTile(destination))
            {
                gridItem.OnFinish();
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
            AudioManager.Instance.OnPushItem();

            return true;
        }

        private List<Tile> GetTiles(Transform gridContainer)
        {
            return gridContainer.GetComponentsInChildren<Tile>().ToList();
        }

        private void MoveItem(GridItem gridItem, Vector2Int destination)
        {
            // Remove the piece from the current position
            _gridItems.Remove(gridItem.Coordinates);

            // Update coordinates and grid position
            gridItem.ChangeCoordinates(destination);
            _gridItems[destination] = gridItem;
            Debug.Log($"[{GetType().Name}] Moved item '{gridItem.name}' to coordinates {destination}");
        }

        private void RegisterTiles(List<Tile> tiles)
        {
            tiles.ForEach(RegisterTile);
        }

        private void RegisterTile(Tile tile)
        {
            if (_tiles.ContainsKey(tile.Coordinates)) throw new Exception($"Cannot register tile at coordinate {tile.Coordinates} - Another tile is already occupying that space");
            _tiles[tile.Coordinates] = tile;
            Debug.Log($"[{GetType().Name}] Registered tile '{tile.name}' at coordinates {tile.Coordinates}");
        }
    }
}