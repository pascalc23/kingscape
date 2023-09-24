using Common;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusicManager : Singleton<BackgroundMusicManager>
    {
        [SerializeField] private AudioClip music;
        [SerializeField] private bool playOnLoad;

        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
            DeactivateOnDevelopment();
            if (playOnLoad)
            {
                PlayMusic();
            }
        }

        public void PlayMusic()
        {
            PlayMusic(music);
        }

        public float GetVolume()
        {
            return _audioSource.volume;
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = Mathf.Clamp01(volume);
        }

        private void PlayMusic(AudioClip musicClip)
        {
            if (_audioSource.enabled == false) return;
            _audioSource.Stop();
            _audioSource.clip = musicClip;
            _audioSource.Play();
        }

        private void DeactivateOnDevelopment()
        {
#if UNITY_EDITOR && NO_MUSIC
            _audioSource.volume = 0f;
#endif
        }
    }
}