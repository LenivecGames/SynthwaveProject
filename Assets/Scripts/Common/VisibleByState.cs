using UnityEngine;
using System.Collections;
using System;

namespace NeonSpace
{
    public class VisibleByState : MonoBehaviour
    {
        public GameState GameState;
        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState == GameState)
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
