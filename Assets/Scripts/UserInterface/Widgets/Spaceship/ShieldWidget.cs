using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class ShieldWidget : Widget
    {
        [SerializeField]
        private Slider _EnergySlider;
        [SerializeField]
        private Shield _Shield;
        // Use this for initialization
        protected override void Start()
        {
            _EnergySlider.minValue = 0;
            _EnergySlider.maxValue = _Shield.ShieldConfig.Energy;
            _EnergySlider.value = _Shield.CurrentEnergy;
            _Shield.OnChangeEnergyEvent += OnChangeEnergyEventHandler;
        }

        private void OnChangeEnergyEventHandler()
        {
            _EnergySlider.value = _Shield.CurrentEnergy;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _Shield.OnChangeEnergyEvent -= OnChangeEnergyEventHandler;
        }
    }
}
