using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace NeonSpace
{
    [Serializable]
    public class UnityBoolEvent : UnityEvent<bool> { }

    public class GameStateCallback : MonoBehaviour
    {
        public GameState GameState;
        public UnityBoolEvent OnStateEvent = new UnityBoolEvent();

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState.Equals(GameState))
            {
                OnStateEvent.Invoke(true);
            }
            else
            {
                OnStateEvent.Invoke(false);
            }
        }
    }
}
