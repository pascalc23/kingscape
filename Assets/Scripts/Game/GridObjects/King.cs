using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class King : ActiveGridItem
    {
        [SerializeField] private Vector2Int forwardVector;

        protected override void OnHeartbeat(int heartbeat)
        {
            Move(forwardVector);
        }

        protected override void OnHalt()
        {
            GameManager.Instance.TriggerLoseCondition();
        }

        public override void OnFinish()
        {
            GameManager.Instance.TriggerWinCondition();
        }
    }
}