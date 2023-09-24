using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private List<Level> levelOrder;

        public bool GameRunning { get; private set; }

        public UnityEvent<Level> onLevelLoaded = new UnityEvent<Level>();
        public UnityEvent eventLevelComplete = new();
        public UnityEvent eventLevelFailed = new();

        public UnityEvent<int> eventHeartbeat = new();
        public UnityEvent<int> eventAfterHeartbeat = new();

        private GridManager _gridManager;
        private GridItemSpawner _gridItemSpawner;
        private int _heartbeat;
        private Level _activeLevel;

        public float TimeBetweenHeartbeats => timeBetweenHeartbeats;

        private void Start()
        {
            _gridManager = GridManager.Instance;
            _gridItemSpawner = GridItemSpawner.Instance;
        }

        public void LoadLevelFromMainMenu(int levelIdx)
        {
            LoadLevel(levelOrder[levelIdx]);
        }

        public void LoadNextLevel()
        {
            var nextLevelIdx = levelOrder.IndexOf(_activeLevel) + 1;
            if (levelOrder.Count < nextLevelIdx)
            {
                Debug.LogError($"NextLevelIdx {nextLevelIdx} is larger than available levels - no more levels to load!");
                return;
            }
            LoadLevel(levelOrder[nextLevelIdx]);
        }

        /// <summary>
        /// Loads a new level.
        /// </summary>
        /// <param name="gridContainer">The container in which the levels grid is hosted. Expects 'Tiles' as children.</param>
        public void LoadLevel(Level level)
        {
            Debug.Log($"[{GetType().Name}] Loading Level '{level.title}'");
            Reset();

            level.levelCam.gameObject.SetActive(true);
            if (_activeLevel != null)
            {
                _activeLevel.levelCam.gameObject.SetActive(false);
            }

            _gridItemSpawner.LoadLevel(level);
            _gridManager.LoadLevel(level);
            _activeLevel = level;

            onLevelLoaded?.Invoke(level);
        }

        /// <summary>
        /// Starts the current level with the <paramref name="itemPrefabs"/> that the player selected in the given order.
        /// </summary>
        public void StartLevel(MovingGridItem[] itemPrefabs)
        {
            Debug.Log($"[{GetType().Name}] Starting level '{_activeLevel.title}'");
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
            Debug.Log($"[{GetType().Name}] Resetting level '{_activeLevel.title}'");
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
            Debug.Log($"[{GetType().Name}] Starting level heartbeat");
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