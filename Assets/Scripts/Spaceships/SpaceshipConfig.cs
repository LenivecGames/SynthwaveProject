using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [System.Serializable]
    public class SpaceshipConfig : Config
    {
        public Sprite Sprite { get { return _Sprite; } }
        [SerializeField]
        private Sprite _Sprite;
        public Vector2 Speed { get { return _Speed; } }
        [SerializeField]
        private Vector2 _Speed;
        

        public SpaceshipConfig(string name,int price, Sprite sprite, Vector2 speed) : base(name, price)
        {
            if(sprite == null || speed == null)
            {
                throw new System.ArgumentNullException();
            }
            _Sprite = sprite;
            _Speed = speed;
        }
    }
}