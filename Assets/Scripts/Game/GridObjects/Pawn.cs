using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class Pawn : ActiveGridItem
    {
        [SerializeField] private Vector2Int forwardVector;

        protected override void OnHeartbeat(int heartbeat)
        {
            Move(forwardVector);
        }

        public override void OnFinish()
        {
            AnimateDestroy();
        }
    }
}