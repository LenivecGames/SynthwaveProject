using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeonSpace
{
    public class Weapon : MonoBehaviour
    {

        public delegate void WeaponShootDelegate();
        public event System.Action OnAmmoChangedEvent;

        public Shell Shell;
        public ParticleSystem MuzzleFlash;

        public int PressureTime { get; private set; }
        public int Ammo { get; private set; }
        private float _ReloadTime = 2f;
        private bool _CanShoot = false;

        private void OnEnable()
        {
            StartCoroutine(Reload());
        }

        private void Start()
        {
            Ammo = 30;
        }

        public void Shoot(int pressureTime)
        {
            if (_CanShoot && Ammo > 0)
            {
                Instantiate(Shell, transform.position, Shell.transform.rotation);
                Destroy(Instantiate(MuzzleFlash.gameObject, transform), MuzzleFlash.main.duration);

                Ammo--;
                _CanShoot = false;

                StartCoroutine(Reload());

                OnAmmoChangedEvent?.Invoke();
            }
        }

        public void AddAmmo(int value)
        {
            Ammo += Mathf.Abs(value);
            OnAmmoChangedEvent?.Invoke();
        }

        public void Configure(WeaponConfig config)
        {

        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(_ReloadTime);
            _CanShoot = true;
        }

    }

}