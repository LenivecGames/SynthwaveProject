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
            _AmmoText.text = _Weapon.Ammo.ToString(); ;
        }

        private void OnWeaponShootHandler()
        {
            _AmmoText.text = _Weapon.Ammo.ToString();
        }
    }
}