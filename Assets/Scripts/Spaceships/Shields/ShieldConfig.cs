using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [System.Serializable]
    public class ShieldConfig : Config
    {
        public readonly Sprite Sprite;
        public readonly int Energy;

        public ShieldConfig(string name,int price, Sprite sprite, int energy) : base(name, price)
        {
            Sprite = sprite;
            Energy = Mathf.Abs(energy);
        }
    }
}
