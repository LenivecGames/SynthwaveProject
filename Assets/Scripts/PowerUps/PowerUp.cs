using UnityEngine;
using System.Collections;
using System;

namespace NeonSpace
{
    public abstract class PowerUp : MonoBehaviour
    {
        protected abstract void Use(Spaceship spaceship);
    }
}
