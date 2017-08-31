using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NeonSpace.Obstacles;

namespace NeonSpace
{
    public class LevelGenerator : MonoBehaviour
    {
        [System.Serializable]
        private struct ObstacleProbability
        {
            public Obstacle Obstacle;
            [Range(0, 1)]
            public float BaseProbability;
            [System.NonSerialized]
            public float NormalizedProbability;
        }

        //[SerializeField]
        //private Camera _Camera;
        [SerializeField]
        private ObstacleProbability[] _Obstacles;
        [SerializeField, Range(0, 1)]
        private float _NormalizationRate;
        [SerializeField]
        private float _SpawnInterval;
        [SerializeField]
        private float MinY;
        [SerializeField]
        private float MaxY;

        private bool _CanGenerateObstacle;
        private float _TimeSinceLastSpawn;
        private List<Obstacle> _SpawnedObstacles = new List<Obstacle>();

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        void Start()
        {
            for (ushort i = 0; i < _Obstacles.Length; ++i)
                _Obstacles[i].NormalizedProbability = _Obstacles[i].BaseProbability;
        }

        void Update()
        {
            if (_CanGenerateObstacle)
            {
                _TimeSinceLastSpawn += Time.deltaTime;
                if (_TimeSinceLastSpawn > _SpawnInterval)
                {
                    Spawn();
                    _TimeSinceLastSpawn = 0;
                }
            }
        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            _CanGenerateObstacle = gameStateMessage.GameState == GameState.Playing;
        }

        private void Spawn()
        {
            _Obstacles = _Obstacles.OrderByDescending(s => s.NormalizedProbability).ToArray();

            if (Random.value < _Obstacles[0].NormalizedProbability)
            {
                //Obstacle obstacle = Instantiate(_Obstacles[0].Obstacle, new Vector2(_Camera.orthographicSize * _Camera.aspect, CalculateSpawnPoint()), Quaternion.identity);

                _SpawnedObstacles = _SpawnedObstacles.Where(s => s != null).ToList();

                _SpawnedObstacles.Add(Instantiate(_Obstacles[0].Obstacle, new Vector2(transform.position.x, CalculateSpawnPoint()), Quaternion.identity));
                NormalizeProbabilities();
                _Obstacles[0].NormalizedProbability = _Obstacles[0].BaseProbability;
            }
        }

        private float CalculateSpawnPoint()
        {
            float minY = (MinY + MaxY) / 2;
            float minValue = float.MaxValue;

            foreach (float y in GetDistFuncDerivativeSolutions(MinY, MaxY))
            {
                float value = _SpawnedObstacles.Sum(o => 1 / ((transform.position.x - o.transform.position.x) * Mathf.Abs(o.transform.position.y - y)));
                if (value < minValue)
                {
                    minValue = value;
                    minY = y;
                }
            }
            return minY;
        }

        float CalcPrecision = 0.1f;

        private IEnumerable<float> GetDistFuncDerivativeSolutions(float min, float max)
        {
            if (Mathf.Abs(min - max) > CalcPrecision)
            {
                float middle = (min + max) / 2;

                float value = 0;
                foreach (Obstacle obst in _SpawnedObstacles)
                {
                    float yiSubY = (obst.transform.position.y - middle);
                    value += yiSubY / ((transform.position.x - obst.transform.position.x) * Mathf.Pow(Mathf.Abs(yiSubY), 3));
                }

                if (Mathf.Abs(value) <= CalcPrecision)
                    return new float[] { middle };

                return GetDistFuncDerivativeSolutions(min, middle).Concat(GetDistFuncDerivativeSolutions(middle + Mathf.Epsilon, max));
            }

            return new float[0];
        }

        private void NormalizeProbabilities()
        {
            for (ushort i = 0; i < _Obstacles.Length; ++i)
                _Obstacles[i].NormalizedProbability += _NormalizationRate;
        }
    }
}
