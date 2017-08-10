using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [System.Serializable]
    public class WeaponConfig : Config
    {
        public readonly float ReloadTime;
        public readonly float WarmupTime;
        public readonly Texture2D StampTexture;
        public readonly ParticleSystem MuzzleFlash;
        public readonly Shell Shell;

        public WeaponConfig(string name,int price, float reloadTime, float warmupTime) : base(name, price)
        {
            ReloadTime = reloadTime;
            WarmupTime = warmupTime;
        }
    }
}
