using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        private void OnPlayButtonClicked()
        {
            LevelLoader.Instance.LoadLevel(0);
        }
    }
}