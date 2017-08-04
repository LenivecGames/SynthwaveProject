using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public abstract class AdvertisementWidget : Widget
    {
        [SerializeField]
        private bool EnableTestMode = true;
        [SerializeField]
        string placementID = "rewardedVideo";
        [SerializeField]
        string gameID_iOS = "1465523";
        [SerializeField]
        string gameID_Android = "1465522";



        protected override void Start()
        {
            if (Advertisement.isSupported && !Advertisement.isInitialized)
            {
#if UNITY_EDITOR
                Advertisement.Initialize(gameID_Android, true);
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
			Advertisement.Initialize(gameID_Android);
#elif UNITY_IOS
			Advertisement.Initialize(gameID_iOS);
#endif
            }
        }

        protected void ShowAd(System.Action<ShowResult> resultHandler)
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            if (Advertisement.IsReady(placementID))
            {
                ShowOptions options = new ShowOptions { resultCallback = resultHandler };
                Advertisement.Show(placementID, options);
            }
#endif
        }
    }
}
