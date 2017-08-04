using UnityEngine;
using System.Collections;

using Destructible2D;

namespace NeonSpace.Obstacles
{
    [RequireComponent(typeof(D2dDestructible), typeof(D2dCollider))]
    public class Asteroid : Obstacle
    {
        private D2dDestructible _DestructibleSprite;
        private D2dAutoCollider _AutoCollider;
        private float _RotationAngle;
        private float _Speed;
        private int _Integrality;

        [SerializeField]
        private ParticleSystem CollisionParticles;
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            _DestructibleSprite = GetComponent<D2dDestructible>();
            _AutoCollider = GetComponent<D2dAutoCollider>();
            _RotationAngle = Random.Range(0f, 45f);
            _Speed = Random.Range(-1, -10);
            _Rigidbody.AddTorque(_Rigidbody.mass * _RotationAngle);
            _Rigidbody.AddForce(new Vector2(_Rigidbody.mass * _Speed, _Rigidbody.position.y), ForceMode2D.Force);

            _Health = Mathf.FloorToInt((float)_DestructibleSprite.AlphaCount / 100);
            _Damage = Mathf.FloorToInt((float)_DestructibleSprite.AlphaCount / 100);
            _Integrality = Mathf.FloorToInt(((float)_DestructibleSprite.AlphaCount / (float)_DestructibleSprite.OriginalAlphaCount) * 100);

            //_Magnitude = _Rigidbody.velocity.magnitude;
            if (_Integrality < 1)
            {
                Destroy(gameObject);
            }
            

        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);

            Rigidbody2D obstacleRigidbody = collision.collider.attachedRigidbody;
            ContactPoint2D contactPoint = collision.contacts[0];
            ParticleSystem particles = Instantiate(CollisionParticles, contactPoint.point, Quaternion.identity);
            Destroy(particles.gameObject, particles.main.duration);
            obstacleRigidbody.AddForce(new Vector2(obstacleRigidbody.transform.position.y * collision.relativeVelocity.x, obstacleRigidbody.transform.position.y * collision.relativeVelocity.x), ForceMode2D.Force);
            obstacleRigidbody.inertia = 0.2f;
        }

        // Update is called once per frame
        protected void FixedUpdate()
        {
            //_Rigidbody.MoveRotation(_RotationAngle);
            //_Rigidbody.MovePosition(new Vector2(_Speed, _Rigidbody.position.y));
            //Debug.Log((_Rigidbody.mass * _Rigidbody.velocity.magnitude));
        }

        protected override void OnDamageReceived(int value)
        {
            base.OnDamageReceived(value);
            _Health = Mathf.FloorToInt((float)_DestructibleSprite.AlphaCount / 100);
            _Damage = Mathf.FloorToInt((float)_DestructibleSprite.AlphaCount / 100);
            _Integrality = Mathf.FloorToInt(((float)_DestructibleSprite.AlphaCount / (float)_DestructibleSprite.OriginalAlphaCount) * 100);
        }
    }
}
