using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private AboutUsPanel aboutUsPanel;
        [SerializeField] private Button playButton;
        [SerializeField] private Button aboutUsButton;

        private void Start()
        {
            QualitySettings.vSyncCount = 1;
            playButton.onClick.AddListener(GoToGame);
            aboutUsButton.onClick.AddListener(aboutUsPanel.Show);
        }

        private void GoToGame()
        {
            SceneManager.LoadScene("Cemetery");
        }
    }
}