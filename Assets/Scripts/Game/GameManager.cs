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

        public bool GameRunning { get; private set; }

        public UnityEvent<int> eventHeartbeat = new();
        public UnityEvent<int> eventAfterHeartbeat = new();

        private GridManager _gridManager;
        private int _heartbeat;

        private void Start()
        {
            _gridManager = GridManager.Instance;
            StartGame();
        }

        private void StartGame()
        {
            if (!_gridManager.IsLevelReady()) throw new Exception("Cannot start game - Level is not ready");
            GameRunning = true;
            StartHeartbeat();
        }

        private void StartHeartbeat()
        {
            _heartbeat = 0;
            StartCoroutine(Heartbeat());
        }

        private IEnumerator Heartbeat()
        {
            while (GameRunning)
            {
                yield return new WaitForSeconds(timeBetweenHeartbeats);

                // Invoke heartbeat event that triggers all pawn movements
                eventHeartbeat.Invoke(_heartbeat);
                Debug.Log($"[{GetType().Name}] Heartbeat {_heartbeat}");
                eventAfterHeartbeat.Invoke(_heartbeat);
                _heartbeat++;
            }
        }

        public void TriggerWinCondition()
        {
            GameRunning = false;
            OnLevelComplete();
        }

        public void TriggerLoseCondition()
        {
            GameRunning = false;
            OnGameLost();
        }

        private void OnLevelComplete()
        {
            Debug.Log("LEVEL IS WON");
        }

        private void OnGameLost()
        {
            Debug.Log("King could not move anymore - game is over");
        }
    }
}