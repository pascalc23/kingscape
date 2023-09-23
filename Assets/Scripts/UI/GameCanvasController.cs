using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField]
    private UnitQueue unitQueue;
    [SerializeField]
    private Button playButton;

    private GameManager gameManager => GameManager.Instance;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        gameManager.StartLevel(unitQueue.GetQueue());
    }
}
