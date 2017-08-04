using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [System.Serializable]
    public class ShieldConfig
    {
        public Sprite Sprite;
        public int Energy;

        public ShieldConfig(Sprite sprite, int energy)
        {
            Sprite = sprite;
            Energy = Mathf.Abs(energy);
        }
    }
}
