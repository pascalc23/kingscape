using System;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts.Game.Grid;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private float timeBetweenHeartbeats = 1f;

        public UnityEvent eventHeartbeat = new();

        private GridManager _gridManager;
        private bool _gameRunning;
        private Vector2Int _previousKingPosition;

        private void Start()
        {
            _gridManager = GridManager.Instance;
            StartGame();
        }

        private void StartGame()
        {
            if (!_gridManager.IsLevelReady()) throw new Exception("Cannot start game - Level is not ready");
            _gameRunning = true;
            StartHeartbeat();
        }

        private void StartHeartbeat()
        {
            StartCoroutine(Heartbeat());
        }

        private IEnumerator Heartbeat()
        {
            while (_gameRunning)
            {
                yield return new WaitForSeconds(timeBetweenHeartbeats);

                // Invoke heartbeat event that triggers all pawn movements
                eventHeartbeat.Invoke();
                Debug.Log($"[{GetType().Name}] Heartbeat");

                // Check win and lose conditions
                CheckWinCondition();
                CheckLoseCondition();

                // Store the kings current position
                _previousKingPosition = _gridManager.king.Coordinates;
            }
        }

        private void CheckWinCondition()
        {
            if (_gridManager.king.Coordinates == _gridManager.levelFinishTile.Coordinates)
            {
                _gameRunning = false;
                OnLevelComplete();
            }
        }

        private void OnLevelComplete()
        {
            Debug.Log("LEVEL IS WON");
        }

        private void CheckLoseCondition()
        {
            // Game not running anymore - probably because the win condition already triggered
            if (!_gameRunning) return;

            if (_gridManager.king.Coordinates == _previousKingPosition)
            {
                _gameRunning = false;
                OnGameLost();
            }
        }

        private void OnGameLost()
        {
            Debug.Log("King could not move anymore - game is over");
        }
    }
}