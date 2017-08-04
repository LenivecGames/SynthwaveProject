using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class SettingsPanel : MonoBehaviour
    {
        public Slider SoundSlider;
        public Toggle SoundToggle;

        // Use this for initialization
        void Start()
        {
            SoundSlider.minValue = 0;
            SoundSlider.maxValue = 1;
            SoundSlider.value = 1;
            SoundSlider.wholeNumbers = false;
            SoundSlider.onValueChanged.AddListener(OnSoundSliderHandler);
            SoundToggle.onValueChanged.AddListener(OnSoundToggleHandler);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnSoundSliderHandler(float value)
        {
            AudioListener.volume = SoundSlider.value;
        }
        private void OnSoundToggleHandler(bool value)
        {
            if (value)
            {
                AudioListener.volume = 0;
                SoundSlider.interactable = false;
            }
            else
            {
                SoundSlider.interactable = true;
                AudioListener.volume = SoundSlider.value;
            }
        }
    }
}
