using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NeonSpace.UserInterface
{
    public class WeaponWidget : Widget
    {
        [SerializeField]
        private Weapon _Weapon;
        [SerializeField]
        private Text _AmmoText;
        [SerializeField]
        private Slider _ReloadSlider;


        // Use this for initialization
        protected override void Start()
        {
            _Weapon.OnAmmoChangedEvent += OnWeaponShootHandler;
            _AmmoText.text = "0";
            _AmmoText.text = _Weapon.Ammo.ToString();

            _ReloadSlider.minValue = 0;
            _ReloadSlider.maxValue = _Weapon._Config.ReloadTime;
        }

        private void Update()
        {
            if(!_Weapon.CanShoot)
            {
                _ReloadSlider.maxValue = _Weapon._Config.ReloadTime;
                _ReloadSlider.value = Mathf.Lerp(_ReloadSlider.minValue, _ReloadSlider.maxValue, _Weapon._Config.ReloadTime/10);
            }
        }

        private void OnWeaponShootHandler()
        {
            _AmmoText.text = _Weapon.Ammo.ToString();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _Weapon.OnAmmoChangedEvent -= OnWeaponShootHandler;
        }
    }
}