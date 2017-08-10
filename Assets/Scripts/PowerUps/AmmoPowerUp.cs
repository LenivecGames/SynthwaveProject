using UnityEngine;
using System.Collections;
using System;

namespace NeonSpace
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class AmmoPowerUp : PowerUp
    {
        public int valueToIncrease;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Spaceship spaceship = collision.collider.attachedRigidbody.gameObject.GetComponent<Spaceship>();
            if(spaceship != null)
            {
                Use(spaceship);
            }
        }

        protected override void Use(Spaceship spaceship)
        {
            spaceship.Weapon.AddAmmo(valueToIncrease);
            Destroy(gameObject);
        }
    }
}
