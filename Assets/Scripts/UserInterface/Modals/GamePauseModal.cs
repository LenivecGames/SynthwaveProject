using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace NeonSpace.UserInterface
{
    public class GamePauseModal : MonoBehaviour
    {

        private void OnEnable()
        {

        }

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if (gameStateMessage.GameState == GameState.Pause)
            {
                gameObject.SetActive(true);

            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
