using UnityEngine;
using System.Collections;

namespace NeonSpace.GameDebug
{
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class FpsCounter : MonoBehaviour
    {
        private UnityEngine.UI.Text _FpsLabel;

        private int _Frames;
        private float _Counter;
        private float _Fps;

        private void Start()
        {
            _FpsLabel = gameObject.GetComponent<UnityEngine.UI.Text>();
        }

        private void Update()
        {
            _Counter += Time.deltaTime;
            _Frames += 1;

            if (_Counter >= 1.0f)
            {
                _Fps = (float)_Frames / _Counter;

                _FpsLabel.text = "FPS: " + Mathf.FloorToInt(_Fps).ToString();

                _Counter = 0.0f;
                _Frames = 0;
            }
        }
    }
}
