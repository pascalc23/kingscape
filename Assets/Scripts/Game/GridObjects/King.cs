using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class King : ActiveGridItem
    {
        [SerializeField] private Vector2Int forwardVector;
        protected override void OnHeartbeat()
        {
            Move(forwardVector);
        }
    }
}