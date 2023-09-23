using System.Collections;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private float timeBetweenHeartbeats = 1f;

        public UnityEvent eventHeartbeat = new();

        private bool _gameRunning;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
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