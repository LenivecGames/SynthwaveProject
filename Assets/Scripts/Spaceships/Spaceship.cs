using UnityEngine;
using System.Collections;

using CoonGames;
using LiteDB;
namespace NeonSpace
{
    [DisallowMultipleComponent, RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(PolygonCollider2D))]
    public class Spaceship : MonoBehaviour, IDamageable
    {
        
        [Prefab]
        public GameObject Prefab;

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

            

        }

        private void Update()
        {

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
                EventManager.Publish<GameStateMessage>(new GameStateMessage(GameState.GameOver));
            }
            _Shield.DecreaseEnergy(value);
            Handheld.Vibrate();
        }

        public void AddAmmo(int value)
        {
            _Weapon.AddAmmo(value);
        }

        private void Move(Vector2 speed)
        {
            _Rigidbody.velocity = new Vector2(speed.x,speed.y);
        }

        public void Move(MoveDirectionType directionType)
        {
            switch(directionType)
            {
                case MoveDirectionType.Down:
                    Move(new Vector2(0, -_SpaceshipConfig.Speed.y));
                    break;
                case MoveDirectionType.Left:
                    Move(new Vector2(-_SpaceshipConfig.Speed.x, 0));
                    break;
                case MoveDirectionType.Right:
                    Move(new Vector2(_SpaceshipConfig.Speed.x, 0));
                    break;
                case MoveDirectionType.Up:
                    Move(new Vector2(0, _SpaceshipConfig.Speed.y));
                    break;
            }
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
            if (gameStateMessage.GameState == GameState.Launch)
            {
                _Shield.IncreaseEnergy(_Shield.MaxEnergy);
                transform.position = Vector3.zero;
            }
        }
    }
}