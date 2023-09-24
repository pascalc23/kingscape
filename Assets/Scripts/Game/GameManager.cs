using System;
using System.Collections;
using Audio;
using Common;
using Game.Grid;
using Game.GridObjects;
using Game.Levels;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private float timeBetweenHeartbeats = 1f;

        public bool GameRunning { get; private set; }

        public UnityEvent eventLevelComplete = new();
        public UnityEvent eventLevelFailed = new();

        public UnityEvent<int> eventHeartbeat = new();
        public UnityEvent<int> eventAfterHeartbeat = new();

        private GridManager _gridManager;
        private int _heartbeat;

        public float TimeBetweenHeartbeats => timeBetweenHeartbeats;

        private void Start()
        {
            _gridManager = GridManager.Instance;
        }

        /// <summary>
        /// Loads a new level.
        /// </summary>
        public void LoadLevel(LevelSO level)
        {
            Reset();
            _gridManager.LoadLevel(level);
        }

        /// <summary>
        /// Starts the current level with the <paramref name="itemPrefabs"/> that the player selected in the given order.
        /// </summary>
        public void StartLevel(GridItem[] itemPrefabs)
        {
            if (!_gridManager.IsLevelReady()) throw new Exception("Cannot start level - Level is not ready");
            if (itemPrefabs == null || itemPrefabs.Length == 0) throw new Exception("Cannot start level - no item prefabs provided");
            GridItemSpawner.Instance.SetItemsToSpawn(itemPrefabs);
            GameRunning = true;
            StartHeartbeat();
        }

        /// <summary>
        /// Resets the current level so the player can try again
        /// </summary>
        public void ResetLevel()
        {
            Reset();
            _gridManager.ResetLevel();
        }

        private void Reset()
        {
            GameRunning = false;
            StopAllCoroutines();
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
            eventLevelComplete.Invoke();
            AudioManager.Instance.OnLevelFinished(true);
            Debug.Log("LEVEL IS WON");
        }

        public void TriggerLoseCondition()
        {
            GameRunning = false;
            eventLevelFailed.Invoke();
            AudioManager.Instance.OnLevelFinished(false);
            Debug.Log("King could not move anymore - game is over");
        }
    }
}