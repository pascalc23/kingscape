using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class Pawn : ActiveGridItem
    {
        protected override void Initialize()
        {
        }

        protected override void OnHeartbeat()
        {
            Move(new Vector2Int(-1, 0));
        }
    }
}