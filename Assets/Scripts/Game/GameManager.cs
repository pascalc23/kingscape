using System;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts.Game.Grid;
using Assets.Scripts.Game.GridObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private float timeBetweenHeartbeats = 1f;
        [SerializeField] private GridItem[] gridItems;

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
            StartLevel(gridItems);
        }

        public void StartLevel(GridItem[] itemPrefabs)
        {
            if (!_gridManager.IsLevelReady()) throw new Exception("Cannot start level - Level is not ready");
            if (itemPrefabs == null || itemPrefabs.Length == 0) throw new Exception("Cannot start level - no item prefabs provided");
            GridItemSpawner.Instance.SetItemsToSpawn(itemPrefabs);
            GameRunning = true;
            StartHeartbeat();
        }

        public void ResetLevel()
        {
            _gridManager.ResetLevel();
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
            Debug.Log("LEVEL IS WON");
        }

        public void TriggerLoseCondition()
        {
            GameRunning = false;
            eventLevelFailed.Invoke();
            Debug.Log("King could not move anymore - game is over");
        }
    }
}