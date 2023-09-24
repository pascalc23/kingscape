using System.Threading.Tasks;
using UnityEngine;

namespace Game.GridObjects
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class MovingGridItem : ActiveGridItem
    {
        [SerializeField] private GameObject model;
        [SerializeField] private AudioClip moveSfx;

        protected Vector2Int forwardVector;
        protected bool isHalted;
        protected AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Initialize(Vector2Int coordinates, Vector2Int forwardVector)
        {
            base.Initialize(coordinates);
            this.forwardVector = forwardVector;
            model.transform.right = new Vector3(-forwardVector.x, 0, -forwardVector.y);
        }

        public void ChangeDirection(Vector2Int direction)
        {
            forwardVector = direction;
            model.transform.right = new Vector3(-direction.x, 0, -direction.y);
            // TODO: animate
            //model.transform.DORotate(new Vector3(-direction.x, 0, -direction.y), 0.1f);
        }

        protected override void OnHeartbeat(int heartbeat)
        {
            if (isHalted) return;
            Interact(forwardVector); // Try to interact first
            Move(forwardVector);
        }

        private void Move(Vector2Int direction)
        {
            Vector2Int destination = Coordinates + direction;
            Debug.Log($"'{name}' is trying to move from {Coordinates} to {destination}");
            if (gridManager.CanMove(this, destination))
            {
                gridManager.Move(this, destination);
                if (moveSfx != null)
                {
                    // wait a bit before playing the SFX - not ideal, needs better configuration
                    Task.Delay(Mathf.RoundToInt(GameManager.Instance.TimeBetweenHeartbeats * 0.7f * 1000));
                    audioSource.PlayOneShot(moveSfx);
                }
            }
            else
            {
                Debug.Log($"'{name}' can't move from {Coordinates} to {destination}");
                OnHalt();
            }
        }


        /// <summary>
        /// Called when the active item is trying to move but can't
        /// </summary>
        protected virtual void OnHalt()
        {
            isHalted = true;
            Debug.Log($"'{name}' halted at {Coordinates}");
        }

        /// <summary>
        /// Called when the active item has reached the finish tile
        /// </summary>
        public virtual void OnFinish()
        {
            isHalted = true;
            DestroySelf();
        }

        protected Vector2Int GetNextCoordinate()
        {
            return Coordinates + forwardVector;
        }
    }
}