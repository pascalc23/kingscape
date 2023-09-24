using UnityEngine;

namespace Game.GridObjects.Obstacles
{
    public class Obstacle : InactiveGridItem
    {
        [SerializeField] private ObstacleTypeSO type;

        public ObstacleTypeSO Type => type;
    }
}