using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Lean.Localization;

namespace NeonSpace.UserInterface
{
    public class SettingsPanel : MonoBehaviour
    {
        public Slider SoundSlider;
        public Toggle SoundToggle;

        public Dropdown LanguageDropdown;

        private Accounts.User _User = GameManager.User;
        // Use this for initialization
        void Start()
        {
            //Sound
            SoundSlider.minValue = 0;
            SoundSlider.maxValue = 1;
            SoundSlider.value = AudioListener.volume;
            SoundSlider.wholeNumbers = false;
            SoundSlider.onValueChanged.AddListener(OnSoundSliderHandler);
            SoundToggle.onValueChanged.AddListener(OnSoundToggleHandler);

            SoundSlider.value = _User.Volume;

            //Language
            LanguageDropdown.AddOptions(LeanLocalization.CurrentLanguages);
            LanguageDropdown.captionText.text = LeanLocalization.CurrentLanguage;
            for(int i = 0; i < LanguageDropdown.options.Count; i++)
            {
                if(LanguageDropdown.options[i].text == LeanLocalization.CurrentLanguage)
                {
                    LanguageDropdown.value = i;
                }
            }
            LanguageDropdown.onValueChanged.AddListener((value) => {
                if (LanguageDropdown.options[value].text == LeanLocalization.CurrentLanguages[value])
                {
                    LeanLocalization.CurrentLanguage = LeanLocalization.CurrentLanguages[value];
                }
            });
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnSoundSliderHandler(float value)
        {
            AudioListener.volume = SoundSlider.value;
            _User.Volume = SoundSlider.value;
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
