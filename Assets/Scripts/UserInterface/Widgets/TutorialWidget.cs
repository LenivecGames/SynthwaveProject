using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class TutorialWidget : MonoBehaviour
    {
        private bool _FirstLaunch = true;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(ShowTutorial());
        }

        // Update is called once per frame
        private IEnumerator ShowTutorial()
        {
            yield return new WaitForSeconds(4f);
            gameObject.SetActive(false);
            _FirstLaunch = false;
        }
    }
}
