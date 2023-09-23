using UnityEngine;

namespace Assets.Scripts.Game.GridObjects
{
    public class King : ActiveGridItem
    {
        [SerializeField] private Vector2Int forwardVector;

        private GameManager _gameManager;
        private Vector2Int _previousCoordinates;

        protected override void OnStart()
        {
            base.OnStart();
            _gameManager = GameManager.Instance;
        }

        protected override void OnHeartbeat(int heartbeat)
        {
            _previousCoordinates = Coordinates;
            Move(forwardVector);
            CheckWinCondition();
            CheckLoseCondition();
        }

        private void CheckWinCondition()
        {
            if (Coordinates == gridManager.levelFinishTile.Coordinates)
            {
                _gameManager.TriggerWinCondition();
            }
        }

        private void CheckLoseCondition()
        {
            // Game not running anymore - probably because the win condition already triggered
            if (!_gameManager.GameRunning) return;

            if (Coordinates == _previousCoordinates)
            {
                _gameManager.TriggerLoseCondition();
            }
        }
    }
}