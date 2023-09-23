using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    private void OnPlayButtonPressed()
    {
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
        gameManager.ResetSpawnedItems();
    }

    private void OnLevelFailed()
    {
        levelCompleteFailedUI.SetActive(true);
        levelCompleteText.SetActive(false);
        levelFailedText.SetActive(true);
        nextLevel.gameObject.SetActive(false);
        gameManager.ResetSpawnedItems();
    }

    private void OnRetryLevelPressed()
    {
        levelCompleteFailedUI.SetActive(false);
        playButton.interactable = true;
        unitSelection.SetUnitSelectionItemsInteractable(true);
    }

    private void OnNextLevelPressed()
    {
        levelCompleteFailedUI.SetActive(false);
        playButton.interactable = true;
        unitSelection.SetUnitSelectionItemsInteractable(true);

        // TODO: Load next level
    }
}
