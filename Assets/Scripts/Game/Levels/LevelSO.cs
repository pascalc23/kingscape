using System.Collections.Generic;
using Cinemachine;
using Game.GridObjects;
using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "Kingscape/Level", order = 3)]
    public class LevelSO : ScriptableObject
    {
        [Tooltip("The level name")]
        public string title;
        [Tooltip("What are we trying to do/teach/challenge in this level - Documentation only")]
        public string description;
        [Tooltip("The scene in which the level lives")]
        public string sceneName;
        [Tooltip("The active camera for this level - we'll do a transition from the previous level's camera to this one")]
        public CinemachineVirtualCamera camera;
        [Tooltip("The prefabs of available pawns for the player to place")]
        public List<MovingGridItem> availablePawns;
        [Tooltip("The number of pawn slots that are available for the player to place pawns in this level")]
        public int pawnSlots;
    }
}