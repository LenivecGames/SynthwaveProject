using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace NeonSpace.Obstacles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Obstacle : MonoBehaviour, IDamageable
    {

        [SerializeField]
        protected int _Damage;
        [SerializeField]
        protected int _Health;
        protected Rigidbody2D _Rigidbody;

        public int Stage { get { return _Stage; } } 
        [SerializeField]
        private int _Stage;

        // Use this for initialization
        protected virtual void Start()
        {
            _Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            IDamageable damageTaker = collision.gameObject.GetComponent<IDamageable>();
            if(damageTaker != null)
            {
                float thisForce = _Rigidbody.mass * _Rigidbody.velocity.magnitude;
                float otherForce = collision.rigidbody.mass * collision.rigidbody.velocity.magnitude;

                damageTaker.TakeDamage(CalculateDamage(thisForce, otherForce));
                //damageTaker.TakeDamage(_Damage);
            }
        }

        public void TakeDamage(int value)
        {
            OnDamageReceived(value);
        }

        protected virtual void OnDamageReceived(int value) {
            int valueToDecrease = Mathf.Abs(value);
            _Health -= valueToDecrease;
            if(_Health < 0)
            {
                Destroy(gameObject);
            }
        }

        private int CalculateDamage(float firstForce, float secondForce)
        {
            if (firstForce > secondForce)
            {
                return Mathf.FloorToInt((float)_Damage * ((secondForce / firstForce) * 100) / 100);
            }
            else
            {
                return Mathf.FloorToInt((float)_Damage * ((firstForce / secondForce) * 100) / 100);
            }
        }
    }
}