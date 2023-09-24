using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThxForPlaying : MonoBehaviour
{
    [SerializeField]
    private Button playAgainButton;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainPressed);
    }

    private void OnPlayAgainPressed()
    {
        LevelLoader.Instance.LoadLevel(0);
    }
}
