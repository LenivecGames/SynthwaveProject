using UnityEngine;
using System.Collections;


namespace NeonSpace
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _MusicAudioClips;
        // Use this for initialization
        void Start()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
            //SoundManager.PlayMusic(_MusicAudioClips[Random.Range(0, _MusicAudioClips.Length)]);
            StartCoroutine(PlayMusic());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {

        }

        private IEnumerator PlayMusic()
        {
            AudioClip audioClip = _MusicAudioClips[Random.Range(0, _MusicAudioClips.Length)];
            SoundManager.PlayMusic(audioClip);
            yield return new WaitForSeconds(audioClip.length);
            StartCoroutine(PlayMusic());
            
        }
    }
}
