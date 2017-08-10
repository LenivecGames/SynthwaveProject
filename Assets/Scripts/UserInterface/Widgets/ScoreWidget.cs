using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class ScoreWidget : Widget
    {
        public bool IsGlobal;
        [SerializeField]
        private Text _ScoreText;

        // Use this for initialization
        protected override void Start()
        {
            _ScoreText.text = "0";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
