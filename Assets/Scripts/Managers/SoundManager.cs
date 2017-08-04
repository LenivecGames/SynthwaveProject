using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeonSpace
{
    [DisallowMultipleComponent]
    public class SoundManager : MonoBehaviour
    {
        public AudioClip[] MenuAudioClips;
        public AudioClip[] GameAudioClips;
        //private List<AudioClip> PlayedAudioClips = new List<AudioClip>();

        private AudioSource MusicAudioSource;
        private AudioSource UIAudioSource;
        private AudioLowPassFilter LowPassFilter;
        //private List<AudioSource> EffectsAudioSources = new List<AudioSource>();

        private void Awake()
        {
            
            MusicAudioSource = gameObject.AddComponent<AudioSource>();
            UIAudioSource = gameObject.AddComponent<AudioSource>();
            LowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
            LowPassFilter.cutoffFrequency = 22000;

            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if (gameStateMessage.GameState == GameState.Menu)
            {
                if (MenuAudioClips.Length != 0)
                {
                    MusicAudioSource.clip = MenuAudioClips[Random.Range(0, MenuAudioClips.Length)];
                    MusicAudioSource.Play();
                }
            }
            else if (gameStateMessage.GameState == GameState.Playing)
            {
                if (GameAudioClips.Length != 0 && !MusicAudioSource.isPlaying)
                {
                    MusicAudioSource.clip = GameAudioClips[Random.Range(0, MenuAudioClips.Length)];
                    MusicAudioSource.Play();
                }
            }

            if (gameStateMessage.GameState == GameState.GameOver || gameStateMessage.GameState == GameState.Pause)
            {
                LowPassFilter.cutoffFrequency = 1200;
            }
            else
            {
                LowPassFilter.cutoffFrequency = 22000;
            }

        }

    }
}
