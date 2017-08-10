using UnityEngine;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

using NeonSpace.Accounts;
using System.Globalization;
namespace NeonSpace
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public static GameState CurrentGameState { get; private set; }
        public static GameState? PreviousGameState { get; private set; }

        public static int CurrentStage {
            get
            {
                if (User != null)
                    return User.Stage;
                else
                    return 0;
            }
        }

        public static User User { get { return _User; } }
        private static User _User = new User(0, 0, 0);

        [SerializeField]
        private int _CountdownTime;
        private Spaceship _Spaceship;

        // Use this for initialization
        private void Awake()
        {
            Time.timeScale = 0;
            //EventManager.EnableDebugLog = true;
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);

            LoadData();
        }

        private void Start()
        {
            EventManager.Publish(new GameStateMessage(GameState.Menu));

            if(string.IsNullOrEmpty(_User.Language))
            {
                if (Lean.Localization.LeanLocalization.CurrentLanguages.Exists(delegate (string value) { return value == CultureInfo.CurrentCulture.Parent.EnglishName; }))
                {
                    Lean.Localization.LeanLocalization.CurrentLanguage = CultureInfo.CurrentCulture.Parent.EnglishName;
                    _User.Language = Lean.Localization.LeanLocalization.CurrentLanguage;
                }
                else
                {
                    Lean.Localization.LeanLocalization.CurrentLanguage = "English";
                    _User.Language = "English";
                }
            }
            AudioListener.volume = _User.VolumeMuted ? 0 : _User.Volume;
            SaveData();
        }

        public void SaveData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/Save.bin", FileMode.Create);
            bf.Serialize(stream, User);
            stream.Close();
        }

        public void LoadData()
        {
            if(File.Exists(Application.persistentDataPath + "/Save.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/Save.bin", FileMode.Open);
                _User = bf.Deserialize(stream) as User;
                stream.Close();
            }
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

            if(gameStateMessage.GameState == GameState.Menu)
            {
                SaveData();
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

        private void OnDestroy()
        {
            
        }
    }
}