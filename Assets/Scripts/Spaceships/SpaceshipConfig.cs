using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [System.Serializable]
    public class SpaceshipConfig
    {
        public readonly Sprite Sprite;
        public readonly Vector2 Speed;
        

        public SpaceshipConfig(Sprite sprite, Vector2 speed)
        {
            if(sprite == null || speed == null)
            {
                throw new System.ArgumentNullException();
            }
            Sprite = sprite;
            Speed = speed;
        }
    }
}