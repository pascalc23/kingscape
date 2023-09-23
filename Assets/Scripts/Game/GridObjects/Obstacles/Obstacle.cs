using UnityEngine;

namespace Assets.Scripts.Game.GridObjects.Obstacles
{
    public class Obstacle : InactiveGridItem
    {
        [SerializeField] private ObstacleTypeSO type;

        public ObstacleTypeSO Type => type;
    }
}