using Game;
using Game.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameCanvasController : MonoBehaviour
    {
        [Header("Ingame controls")]
        [SerializeField]
        private UnitSelection unitSelection;
        [SerializeField]
        private UnitQueue unitQueue;

        [SerializeField]
        private Button playButton;

        [Header("Level Complete/Failed")]
        [SerializeField]
        private GameObject levelCompleteFailedUI;
        [SerializeField]
        private GameObject levelCompleteText;
        [SerializeField]
        private GameObject levelFailedText;
        [SerializeField]
        private Button retryLevel;
        [SerializeField]
        private Button nextLevel;

        private GameManager gameManager => GameManager.Instance;

        private void Awake()
        {
            levelCompleteFailedUI.gameObject.SetActive(false);

            playButton.onClick.AddListener(OnPlayButtonPressed);
            retryLevel.onClick.AddListener(OnRetryLevelPressed);
            nextLevel.onClick.AddListener(OnNextLevelPressed);
        }

        private void Start()
        {
            gameManager.eventLevelComplete.AddListener(OnLevelComplete);
            gameManager.eventLevelFailed.AddListener(OnLevelFailed);

            gameManager.onLevelLoaded.AddListener(OnLevelLoaded);
        }

        private void OnPlayButtonPressed()
        {
            var queue = unitQueue.GetQueue();

            bool hasKing = false;
            foreach (var item in queue)
            {
                if (item == null)
                    continue;
                hasKing |= item.name.ToLower().Contains("king");
            }

            if (!hasKing)
            {
                Debug.Log("At least one unit needs to be a king unit");
                return;
            }

            gameManager.StartLevel(unitQueue.GetQueue());
            unitSelection.SetUnitSelectionItemsInteractable(false);
            playButton.interactable = false;
        }

        private void OnLevelComplete()
        {
            levelCompleteFailedUI.SetActive(true);
            levelCompleteText.SetActive(true);
            levelFailedText.SetActive(false);
            nextLevel.gameObject.SetActive(true);
        }

        private void OnLevelFailed()
        {
            levelCompleteFailedUI.SetActive(true);
            levelCompleteText.SetActive(false);
            levelFailedText.SetActive(true);
            nextLevel.gameObject.SetActive(false);
        }

        private void OnRetryLevelPressed()
        {
            levelCompleteFailedUI.SetActive(false);
            playButton.interactable = true;
            unitSelection.SetUnitSelectionItemsInteractable(true);
            gameManager.ResetLevel();
        }

        private void OnNextLevelPressed()
        {
            levelCompleteFailedUI.SetActive(false);
            playButton.interactable = true;
            unitSelection.SetUnitSelectionItemsInteractable(true);
            gameManager.ResetLevel();

            gameManager.LoadNextLevel();
        }

        private void OnLevelLoaded(Level level)
        {
            unitSelection.OnLevelLoaded(level);
            unitQueue.OnLevelLoaded(level);
        }
    }
}