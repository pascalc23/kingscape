using System.Collections.Generic;
using Cinemachine;
using Game.Grid.Tiles;
using Game.GridObjects;
using UnityEngine;

namespace Game.Levels
{
    public class Level : MonoBehaviour
    {
        [Tooltip("The level name")]
        public string title;
        [Tooltip("What are we trying to do/teach/challenge in this level - Documentation only")]
        public string description;
        [Tooltip("The active camera for this level - we'll do a transition from the previous level's camera to this one")]
        public CinemachineVirtualCamera levelCam;
        [Tooltip("The prefabs of available pawns for the player to place")]
        public List<MovingGridItem> availablePawns;
        [Tooltip("The number of pawn slots that are available for the player to place pawns in this level")]
        public int pawnSlots;

        public Tile startTile;
        public Tile finishTile;

        public Transform gridContainer;

        public Vector2Int startDirection;
    }
}