using System.Collections.Generic;
using Cinemachine;
using Game.GridObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "Kingscape/Level", order = 3)]
    public class LevelSO : ScriptableObject
    {
        public Scene scene;
        public CinemachineVirtualCamera camera;
        public List<MovingGridItem> availablePawns;
    }
}