using UnityEngine;

namespace Game.GridObjects.Obstacles
{
    [CreateAssetMenu(fileName = "Obstacle Type", menuName = "Kingscape/Obstacle Type", order = 2)]
    public class ObstacleTypeSO : ScriptableObject
    {
        public string title;
        public bool isMovable;
    }
}