using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class SyntheseWidget : Widget
    {

        [SerializeField]
        private Text _SyntheseText;

        // Use this for initialization
        protected override void Start()
        {
            _SyntheseText.text = "0";
        }
    }
}
