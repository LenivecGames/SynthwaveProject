using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using NeonSpace.Obstacles;

using Lean;

namespace NeonSpace
{
    public class GeneratorManager : MonoBehaviour
    {

        public float DelayMin, DelayMax;
        public float MinY, MaxY;

        public PowerUp[] PowerUps;
        public Obstacle[] Obstacles;

        public PowerUp[] CurrentStagePowerUps;
        public Obstacle[] CurrentStageObstacles
        {
            get
            {
                List<Obstacle> currentStageObstacles = new List<Obstacle>();
                foreach (Obstacle obstacle in Obstacles)
                {
                    if (obstacle.Stage <= GameManager.CurrentStage)
                    {
                        currentStageObstacles.Add(obstacle);
                    }
                }
                return currentStageObstacles.ToArray();
            }
        }

        private List<Obstacle> _Obstacles = new List<Obstacle>();
        private List<PowerUp> _PowerUps = new List<PowerUp>();

        private bool _CanGenerateObstacle = false;

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        private void Update()
        {

        }

        private IEnumerator GenerateObstacle()
        {
            yield return new WaitForSeconds(Random.Range(DelayMin, DelayMax));
            if (Obstacles.Length != 0)
            {
                Obstacle obstacle = Instantiate(CurrentStageObstacles[Random.Range(0, CurrentStageObstacles.Length)], new Vector2(transform.position.x, Random.Range(MinY, MaxY)), Quaternion.identity);
                _Obstacles.Add(obstacle);
            }
            StartCoroutine(GenerateObstacle());
        }

        private IEnumerator GeneratePowerUp()
        {
            yield return new WaitForSeconds(Random.Range(DelayMin, DelayMax));
            if (PowerUps.Length != 0)
            {
                PowerUp powerUp = Instantiate(PowerUps[Random.Range(0, PowerUps.Length)], new Vector2(transform.position.x, Random.Range(MinY, MaxY)), Quaternion.identity);
                _PowerUps.Add(powerUp);
            }
            StartCoroutine(GeneratePowerUp());
        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState == GameState.Playing)
            {
                _CanGenerateObstacle = true;
                StartCoroutine(GenerateObstacle());
                StartCoroutine(GeneratePowerUp());
            }
            else
            {
                _CanGenerateObstacle = false;
            }

            if(gameStateMessage.GameState == GameState.GameOver)
            {
                foreach(Obstacle obstacle in _Obstacles)
                {
                    if(obstacle != null)
                    {
                        Destroy(obstacle.gameObject);
                    }
                }
                foreach (PowerUp powerUp in _PowerUps)
                {
                    if (powerUp != null)
                    {
                        Destroy(powerUp.gameObject);
                    }
                }
                _Obstacles.Clear();
                _PowerUps.Clear();
                StopAllCoroutines();
            }

        }
    }
}
