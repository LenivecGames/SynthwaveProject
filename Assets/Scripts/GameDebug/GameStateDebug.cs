using UnityEngine;
using System.Collections;

using EasyButtons;
namespace NeonSpace.GameDebug
{
    public class GameStateDebug : MonoBehaviour
    {
        [SerializeField]
        private GameState _GameState = GameManager.CurrentGameState;

        [Button]
        private void SetGameStateButton()
        {
            SetGameState(_GameState);
        }

        private void SetGameState(GameState gameState)
        {
            EventManager.Publish(new GameStateMessage(gameState));
        }

        [Button]
        private void SetGameTime_1()
        {
            Time.timeScale = 1;
        }
        [Button]
        private void SetGameTime_2()
        {
            Time.timeScale = 2;
        }
        [Button]
        private void SetGameTime_3()
        {
            Time.timeScale = 3;
        }
    }
}
