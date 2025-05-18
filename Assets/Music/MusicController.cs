using UnityEngine;

namespace Music
{
    public class MusicController : MonoBehaviour
    {
        #region Singleton

        private static MusicController _instance;

        public static MusicController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<MusicController>();
                    if (_instance == null)
                    {
                        _instance = Instantiate(Resources.Load<MusicController>("MusicController"));
                    }

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void SetMusicVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}