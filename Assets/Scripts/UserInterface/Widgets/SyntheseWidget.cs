using UnityEngine;
using UnityEngine.UI;
using System.Collections;



namespace NeonSpace.UserInterface
{
    public class SyntheseWidget : Widget
    {
        public bool IsGlobal; //true: user.synthese; false: current
        [SerializeField]
        private Text _SyntheseText;

        // Use this for initialization
        protected override void Start()
        {
            _SyntheseText.text = GameManager.User.Synthese.ToString();
        }

        private void OnUserChangedHandler()
        {
            _SyntheseText.text = GameManager.User.Synthese.ToString();
        }
    }
}
