using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("General SFX")]
        [SerializeField] private AudioClip buttonClickSfx;
        [SerializeField] private AudioClip buttonHoverSfx;

        [Header("Pawn Actions SFX")]
        [SerializeField] private AudioClip moveSfx;
        [SerializeField] private AudioClip pushItemSfx;

        [Header("Game End SFX")]
        [SerializeField] private AudioClip levelCompletedSfx;
        [SerializeField] private AudioClip levelFailedSfx;

        public float Volume { get; private set; }
        public UnityEvent<float> eventVolumeChanged = new();

        private AudioSource _audioSource;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
            DeactivateOnDevelopment();
        }

        public void SetVolume(float volume)
        {
            Volume = Mathf.Clamp01(volume);
            _audioSource.volume = Volume;
            eventVolumeChanged.Invoke(Volume);
        }

        public void OnButtonClick()
        {
            PlaySfx(buttonClickSfx);
        }

        public void OnButtonHover()
        {
            PlaySfx(buttonHoverSfx);
        }

        public void OnPushItem()
        {
            PlaySfx(pushItemSfx);
        }

        public void OnMove()
        {
            PlaySfx(moveSfx, 0.5f);
        }

        public void OnLevelFinished(bool playerWon)
        {
            PlaySfx(playerWon ? levelCompletedSfx : levelFailedSfx);
        }

        private void PlaySfx(AudioClip audioClip, float volume = 1.0f)
        {
            if (audioClip == null || _audioSource == null) return;
            _audioSource.PlayOneShot(audioClip, volume);
        }

        private void PlayMultipleSfx(List<AudioClip> audioClips, List<float> waitSecondsBefore, float volume = 1.0f)
        {
            StartCoroutine(PlayMultipleSfxCoroutine(audioClips, waitSecondsBefore, volume));
        }

        private IEnumerator PlayMultipleSfxCoroutine(List<AudioClip> audioClips, List<float> waitSecondsBefore, float volume = 1.0f)
        {
            if (audioClips == null || waitSecondsBefore == null) throw new Exception("Parameter audioClips and waitSecondsBefore must not be null");
            if (audioClips.Count != waitSecondsBefore.Count) throw new Exception("Count of audioClips and waitSecondsBefore must be the same");
            for (int i = 0; i < audioClips.Count; i++)
            {
                yield return new WaitForSeconds(waitSecondsBefore[i]);
                PlaySfx(audioClips[i], volume);
            }
        }

        private void DeactivateOnDevelopment()
        {
#if UNITY_EDITOR && NO_AUDIO
            _audioSource.volume = 0f;
#endif
        }
    }
}