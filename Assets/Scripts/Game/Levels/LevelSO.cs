using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Game.Grid.Tiles;
using Game.GridObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "Kingscape/Level", order = 3)]
    public class LevelSO : ScriptableObject
    {
        [Tooltip("The scene in which the level lives")]
        public Scene scene;
        [Tooltip("The active camera for this level - we'll do a transition from the previous level's camera to this one")]
        public CinemachineVirtualCamera camera;
        [Tooltip("The container in which the levels grid is hosted. Expects 'Tiles' as children.")]
        public Transform levelGrid;
        [Tooltip("The prefabs of available pawns for the player to place")]
        public List<MovingGridItem> availablePawns;

        public List<Tile> GetTiles()
        {
            return levelGrid.GetComponentsInChildren<Tile>().ToList();
        }
    }
}