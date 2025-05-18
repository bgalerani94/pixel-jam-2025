using UnityEngine;
using UnityEngine.UI;

namespace Music
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button muteButton;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite unmuteSprite;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            muteButton.onClick.AddListener(ToggleMute);
        }

        private void ToggleMute()
        {
            audioSource.mute = !audioSource.mute;
            muteButton.image.sprite = audioSource.mute ? muteSprite : unmuteSprite;
        }
    }
}