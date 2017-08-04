using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace
{
    [DisallowMultipleComponent]
    public class UIManager : MonoBehaviour
    {

        public RectTransform WidgetsRect;
        public RectTransform MenuRect;

        public RectTransform PauseModal;
        public RectTransform GameOverModal;

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState == GameState.Menu)
            {
                MenuRect.gameObject.SetActive(true);
            }
            else
            {
                MenuRect.gameObject.SetActive(false);
            }

            switch(gameStateMessage.GameState)
            {

                case GameState.Menu:
                    WidgetsRect.gameObject.SetActive(false);
                    break;
                case GameState.Playing:
                    WidgetsRect.gameObject.SetActive(true);
                    break;
                case GameState.Pause:
                    PauseModal.gameObject.SetActive(true);
                    break;
                case GameState.GameOver:
                    WidgetsRect.gameObject.SetActive(false);
                    GameOverModal.gameObject.SetActive(true);
                    break;
            }
        }
    }

}