using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeonSpace.Shop
{
    public class Shop : MonoBehaviour
    {

        public ConfigDatabase Items { get { return _Items; } }
        [SerializeField]
        private ConfigDatabase _Items;



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
