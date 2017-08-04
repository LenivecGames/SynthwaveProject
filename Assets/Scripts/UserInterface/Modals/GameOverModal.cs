using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class GameOverModal : MonoBehaviour
    {

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
            if(gameStateMessage.GameState == GameState.GameOver)
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
