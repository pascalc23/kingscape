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
                eventHeartbeat.Invoke();
                Debug.Log($"[{GetType().Name}] Heartbeat");
            }
        }
    }
}