using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

using Messaging;

namespace NeonSpace.UserInterface
{
    public class GameOverAdvertisementWidget : AdvertisementWidget
    {
        public Text AdvertisementText;
        public Button DoubleSyntheseButton;

        private bool _AdvertisementShowed = false;

        protected override void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            DoubleSyntheseButton.onClick.AddListener(() => {
                Debug.Log("Ad clicked");
                ShowAd(HandleAdvertisementResult);
            });
        }

        private void HandleAdvertisementResult(ShowResult result)
        {
            Debug.Log("Ad showed");
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                DoubleSyntheseButton.gameObject.SetActive(false);
                AdvertisementText.gameObject.SetActive(true);
                AdvertisementText.text = "Lost internet connection.";
                return;
            }

            if (result == ShowResult.Finished)
            {
                _AdvertisementShowed = true;
                DoubleSyntheseButton.gameObject.SetActive(!_AdvertisementShowed);
                AdvertisementText.gameObject.SetActive(_AdvertisementShowed);

                //int reward = GameManager.Instance.Player.Synthese;

                /*GameManager.Instance.Player.IncreaseSynthese(reward);
                Debug.Assert(GameManager.Instance.Player.Synthese == reward * 2);
                Debug.Log(GameManager.Instance.Player.Synthese + "   " + reward * 2);*/

                //AdvertisementText.text = "Thanks for watching. \n" + "You get: " + GameManager.Instance.Player.Synthese.ToString() + " Synthese.";
            }
            else if (result == ShowResult.Failed)
            {
                _AdvertisementShowed = false;
                Debug.Log(result);
            }
            else if (result == ShowResult.Skipped)
            {
                _AdvertisementShowed = false;
            }
        }


        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if (gameStateMessage.GameState == GameState.GameOver)
            {
                _AdvertisementShowed = false;
                AdvertisementText.gameObject.SetActive(false);
                DoubleSyntheseButton.gameObject.SetActive(true);
            }
        }
    }
}
