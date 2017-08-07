﻿using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [DisallowMultipleComponent, RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(PolygonCollider2D))]
    public class Spaceship : MonoBehaviour, IDamageable
    {

        public SpaceshipConfig _SpaceshipConfig;

        private SpriteRenderer _SpriteRenderer;
        private Rigidbody2D _Rigidbody;
        private PolygonCollider2D _Collider;

        [SerializeField]
        private Shield _Shield;
        [SerializeField]
        private Weapon _Weapon;
        [SerializeField]
        private ParticleSystem _Trail;

        [SerializeField]
        private ParticleSystem _ExplosionParticles;

        [SerializeField]
        private Transform _WeaponMount;
        [SerializeField]
        private Transform _ShieldMount;
        [SerializeField]
        private Transform[] _TrailMounts;

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHander);
        }

        private void Start()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Rigidbody = GetComponent<Rigidbody2D>();
            _Collider = GetComponent<PolygonCollider2D>();

            _Collider.autoTiling = true;

            if(_Trail != null && _TrailMounts != null)
            {
                foreach(Transform mount in _TrailMounts)
                {
                    ParticleSystem trail = Instantiate(_Trail);
                    trail.transform.SetParent(mount);
                    trail.gameObject.SetActive(true);

                }
            }

            ConfigureCore(new SpaceshipConfig(_SpriteRenderer.sprite, new Vector2(6, 2)));
            ConfigureShield(new ShieldConfig(_Shield.SpriteRenderer.sprite, 60));
            ConfigureWeapon(new WeaponConfig(2f));

        }

        private void FixedUpdate()
        {

            if(GameManager.CurrentGameState.Equals(GameState.Launch))
            {
                MoveForward();
            }

            if (GameManager.CurrentGameState.Equals(GameState.Playing))
            {
                _Rigidbody.position = new Vector2(_Rigidbody.position.x, Mathf.Clamp(_Rigidbody.position.y, -Camera.main.orthographicSize + _Collider.bounds.size.y/2, Camera.main.orthographicSize - _Collider.bounds.size.y/2));
                    
                Move(new Vector2(_SpaceshipConfig.Speed.x, _Rigidbody.velocity.y));
            }
        }

        public void TakeDamage(int value)
        {
            if (_Shield.IsDestroyed)
            {
                Coroutiner.Start(Explode());
            }
            _Shield.DecreaseEnergy(value);
            Handheld.Vibrate();
        }

        public IEnumerator Explode()
        {
            ParticleSystem particles = Instantiate(_ExplosionParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(particles, particles.main.duration);
            yield return new WaitForSeconds(particles.main.duration);
            EventManager.Publish<GameStateMessage>(new GameStateMessage(GameState.GameOver));
        }

        public void AddAmmo(int value)
        {
            _Weapon.AddAmmo(value);
        }

        private void Move(Vector2 speed)
        {
            _Rigidbody.velocity = new Vector2(speed.x,speed.y);
        }

        public void MoveForward()
        {
            Move(new Vector2(_SpaceshipConfig.Speed.x, 0));
        }

        public void MoveUp()
        {
            Move(new Vector2(_SpaceshipConfig.Speed.x, _SpaceshipConfig.Speed.y));
        }
        public void MoveDown()
        {
            Move(new Vector2(_SpaceshipConfig.Speed.x, -_SpaceshipConfig.Speed.y));
        }

        public void ConfigureCore(SpaceshipConfig config)
        {
            _SpaceshipConfig = config;
            _SpriteRenderer.sprite = _SpaceshipConfig.Sprite;
        }

        public void ConfigureShield(ShieldConfig config)
        {
            _Shield.Configure(config);
        }

        public void ConfigureWeapon(WeaponConfig config)
        {
            _Weapon.Configure(config);
        }

        public void ConfigureTrail()
        {

        }

        public void Shoot(int pressureTime)
        {
            _Weapon.Shoot(pressureTime);
        }

        public void SetSprite(Sprite sprite)
        {
            _SpriteRenderer.sprite = sprite;
        }

        private void OnGameStateHander(GameStateMessage gameStateMessage)
        {
            if (gameStateMessage.GameState == GameState.Launch || gameStateMessage.GameState == GameState.Menu)
            {
                gameObject.SetActive(true);
                _Shield.IncreaseEnergy(_Shield.MaxEnergy);
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }

            if(gameStateMessage.GameState == GameState.Playing)
            {
                transform.Rotate(new Vector3(-10,0,0)); // ...
                LeanTween.rotateX(gameObject, 10f, 3f).setRecursive(true).setLoopPingPong();
            }
        }
    }
}