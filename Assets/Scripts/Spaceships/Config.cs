using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [System.Serializable]
    public abstract class Config
    {
        public string Name { get { return _Name; } }
        [SerializeField]
        private string _Name;
        public bool Unlocked { get { return _Unlocked; } }
        [SerializeField]
        private bool _Unlocked;
        public int Price { get { return Mathf.Abs(_Price); } }
        [SerializeField]
        private int _Price;
        public Config(string name, int price)
        {
            _Name = name;
            _Price = price;
        }

        public void Unlock(bool value)
        {
            _Unlocked = value;
        }

    }
}
