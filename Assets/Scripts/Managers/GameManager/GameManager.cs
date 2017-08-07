using UnityEngine;
using System.Collections;

using NeonSpace.Accounts;

namespace NeonSpace
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public static GameState CurrentGameState { get; private set; }
        public static GameState? PreviousGameState { get; private set; }

        public static int CurrentStage { get; private set; }

        [SerializeField]
        private int _CountdownTime;

        private User _User = new User(0,0,0);
        private Spaceship _Spaceship;

        // Use this for initialization
        private void Awake()
        {
            Time.timeScale = 0;
            EventManager.EnableDebugLog = true;
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);

        }

        private void Start()
        {
            EventManager.Publish(new GameStateMessage(GameState.Menu));
            CurrentStage = _User.Stage;
        }

        public void SaveData()
        {

        }

        public void LoadData()
        {

        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            PreviousGameState = CurrentGameState;
            CurrentGameState = gameStateMessage.GameState;

            if (gameStateMessage.GameState == GameState.Playing || gameStateMessage.GameState == GameState.Launch)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

            if(CurrentGameState == GameState.Launch)
            {
                StartCoroutine(StartContdown());
            }
        }

        private IEnumerator StartContdown()
        {
            yield return new WaitForSeconds(_CountdownTime);
            EventManager.Publish(new GameStateMessage(GameState.Playing));
        }

        private void OnApplicationPause(bool pause)
        {
            if(pause)
            {
                SaveData();
#if !UNITY_EDITOR
                if (CurrentGameState == GameState.Playing)
                {
                    EventManager.Publish(new GameStateMessage(GameState.Pause));
                }
#endif
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            OnApplicationPause(!focus);
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }
    }
}