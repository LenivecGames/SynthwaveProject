using UnityEngine;
using System.Collections;
using Destructible2D;

namespace NeonSpace
{

    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Shell : MonoBehaviour
    {
        //public ShellExplosion Explosion;

        public string IgnoreTag;

        public Vector2 Speed;

        public ParticleSystem ExplosionParticles;

        private Rigidbody2D _Rigidbody;
        private SpriteRenderer _SpriteRenderer;
        private Collider2D _Collider;

        [Header("Explosion settings")]
        public Texture2D StampTexture;
        public Vector2 StampSize = new Vector2(1, 1);
        public float StampHardness = 1;
        public bool StampRandomDirection = false;
        public bool Raycast = true;
        public float RaycastRadius = 1.0f;
        public int RaycastCount = 32;
        public float ForcePerRay = 1.0f;
        public float DamagePerRay = 1.0f;

        protected virtual void Start()
        {
            _Rigidbody = GetComponent<Rigidbody2D>();
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Collider = GetComponent<Collider2D>();

            _Collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            IDamageable damageTaker = collider.attachedRigidbody?.gameObject.GetComponent<IDamageable>();
            if (damageTaker != null && collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Boss")
            {
                //Instantiate(Explosion, transform.position, Quaternion.identity);
                Explosion();
                ParticleSystem particles = Instantiate(ExplosionParticles, transform.position, transform.rotation);
                Destroy(particles.gameObject, particles.main.duration);

                Destroy(gameObject);
            }
        }

        protected virtual void FixedUpdate()
        {
            _Rigidbody.MovePosition(new Vector2(
                _Rigidbody.position.x + Speed.x * Time.fixedDeltaTime,
                _Rigidbody.position.y + Speed.y * Time.fixedDeltaTime)
                );
        }

        protected virtual void Explosion()
        {
                var stampPosition = transform.position;
                var stampAngle = StampRandomDirection == true ? Random.Range(-180.0f, 180.0f) : 0.0f;

                D2dDestructible.StampAll(stampPosition, StampSize, stampAngle, StampTexture, StampHardness/*, Mask*/);

            if (Raycast == true && RaycastCount > 0)
            {
                var angleStep = 360.0f / RaycastCount;

                for (var i = 0; i < RaycastCount; i++)
                {
                    var angle = i * angleStep;
                    var direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                    var hit = Physics2D.Raycast(transform.position, direction, RaycastRadius/*, Mask*/);
                    var collider = hit.collider;

                    // Make sure the raycast hit something, and that it wasn't a trigger
                    if (collider != null && collider.isTrigger == false)
                    {
                        var strength = 1.0f - hit.fraction; // Do less damage if the hit point is far from the explosion

                        // Add damage?
                        if (DamagePerRay != 0.0f)
                        {
                            var destructible = collider.GetComponentInParent<D2dDestructible>();

                            if (destructible != null)
                            {
                                destructible.Damage += DamagePerRay * strength;
                            }
                        }

                        // Add force?
                        if (ForcePerRay != 0.0f)
                        {
                            var rigidbody2D = collider.attachedRigidbody;

                            if (rigidbody2D != null)
                            {
                                var force = direction * ForcePerRay * strength;

                                rigidbody2D.AddForceAtPosition(force, hit.point);
                            }
                        }
                    }
                }
            }
        }

        private float Divide(float a, float b)
        {
            return Zero(b) == false ? a / b : 0.0f;
        }

        private bool Zero(float v)
        {
            return v == 0.0f;
        }
    }
}
