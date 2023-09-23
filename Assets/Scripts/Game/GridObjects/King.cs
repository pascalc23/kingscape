using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class King : ActiveGridItem
    {
        [SerializeField] private Vector2Int forwardVector;

        private GameManager _gameManager;

        protected override void OnStart()
        {
            base.OnStart();
            _gameManager = GameManager.Instance;
        }

        protected override void OnHeartbeat(int heartbeat)
        {
            Move(forwardVector);
        }

        protected override void OnHalt()
        {
            _gameManager.TriggerLoseCondition();
        }

        public override void OnFinish()
        {
            _gameManager.TriggerWinCondition();
        }
    }
}