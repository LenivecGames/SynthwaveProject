using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [RequireComponent(typeof(Collider2D))]
    public class ShieldPowerUp : PowerUp
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
            spaceship.Shield.IncreaseEnergy(valueToIncrease);
            Destroy(gameObject);
        }

    }
}
