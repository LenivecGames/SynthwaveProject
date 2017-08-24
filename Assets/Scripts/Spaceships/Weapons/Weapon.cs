using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Lean;

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

        public AudioClip ShootSound;

        public WeaponConfig _Config { get; private set; }

        //private float _ReloadTime;
        public bool CanShoot{ get { return _CanShoot; } }
        private bool _CanShoot = true;

        private void OnEnable()
        {

        }

        private void Start()
        {

            Ammo = 30;
        }

        /*protected virtual void BeginShoot()
        {

        }

        protected virtual void CountinueShoot()
        {

        }

        protected virtual void EndShoot()
        {

        }*/

        public void Shoot(int pressureTime)
        {
            if (_CanShoot && Ammo > 0)
            {
                LeanPool.Spawn(Shell, transform.position, Shell.transform.rotation);
                LeanPool.Despawn(LeanPool.Spawn(MuzzleFlash.gameObject, transform.position, transform.rotation, transform), MuzzleFlash.main.duration);
                SoundManager.PlaySound(ShootSound);
                Ammo--;
                _CanShoot = false;

                Coroutiner.Start(Reload());

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
            _Config = config;
            //_ReloadTime = config.ReloadTime;
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(_Config.ReloadTime);
            _CanShoot = true;
        }

    }

}