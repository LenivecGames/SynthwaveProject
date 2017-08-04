using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shield : MonoBehaviour
    {
        public event System.Action OnChangeEnergyEvent;

        [CoonGames.Prefab]
        public ParticleSystem CollisionParticles;

        public ShieldConfig ShieldConfig { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public int MaxEnergy { get {return ShieldConfig.Energy; } }
        public int CurrentEnergy { get; private set; }
        public bool IsDestroyed { get { return CurrentEnergy <= 0; } }

        private bool _Indestructible = false;
        private float _DamageCooldown = 0.2f;

        private Gradient DamageGradient;

        // Use this for initialization
        void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody2D obstacleRigidbody = collision.collider.attachedRigidbody;
            /*foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                ParticleSystem particles = Instantiate(CollisionParticles, contactPoint.point, transform.rotation);
                obstacleRigidbody.AddForce(new Vector2(obstacleRigidbody.transform.position.y * collision.relativeVelocity.x, obstacleRigidbody.transform.position.y * collision.relativeVelocity.x), ForceMode2D.Force);
                obstacleRigidbody.inertia = 0.2f;
                Destroy(particles, particles.main.duration);
            }*/

            {
                ContactPoint2D contactPoint = collision.contacts[0];
                ParticleSystem particles = Instantiate(CollisionParticles, contactPoint.point, Quaternion.identity);
                obstacleRigidbody.AddForce(new Vector2(obstacleRigidbody.transform.position.y * collision.relativeVelocity.x, obstacleRigidbody.transform.position.y * collision.relativeVelocity.x), ForceMode2D.Force);
                obstacleRigidbody.inertia = 0.2f;

                particles.gameObject.transform.SetParent(transform);
                Destroy(particles.gameObject, particles.main.duration);
            }
        }

        public void Configure(ShieldConfig config)
        {
            ShieldConfig = config;
            SpriteRenderer.sprite = config.Sprite;
            CurrentEnergy = config.Energy;
            
            OnChangeEnergyEvent?.Invoke();
        }

        public void IncreaseEnergy(int value)
        {
            int valueToIncrease = Mathf.Abs(value);
            gameObject.SetActive(true);

            CurrentEnergy += value;
            if (CurrentEnergy > MaxEnergy)
            {
                CurrentEnergy = MaxEnergy;
            }
            OnChangeEnergyEvent?.Invoke();
            Debug.Log("Current enrgy: " + CurrentEnergy);
            return;
        }
        public void DecreaseEnergy(int value)
        {
            if (!_Indestructible)
            {
                int valueToDecrease = Mathf.Abs(value);
                CurrentEnergy -= valueToDecrease;
                if (CurrentEnergy <= 0)
                {
                    CurrentEnergy = 0;
                    gameObject.SetActive(false);
                }
                else
                {
                    StartCoroutine(Cooldown());
                }
                OnChangeEnergyEvent?.Invoke();
            }
            return;
        }

        private IEnumerator Cooldown()
        {
            _Indestructible = true;
            yield return new WaitForSeconds(_DamageCooldown);
            _Indestructible = false;
        }
    }
}

