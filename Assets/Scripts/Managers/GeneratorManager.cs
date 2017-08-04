using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using NeonSpace.Obstacles;

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

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState == GameState.Playing)
            {
                _CanGenerateObstacle = true;
                StartCoroutine(GenerateObstacle());
            }
            else
            {
                _CanGenerateObstacle = false;
            }

            if(gameStateMessage.GameState == GameState.GameOver)
            {
                for(int i = 0; i < _Obstacles.Count; i++)
                {
                    if(_Obstacles[i] != null)
                    {
                        Destroy(_Obstacles[i].gameObject);
                    }
                }
                _Obstacles.Clear();
                StopAllCoroutines();
            }

        }
    }
}
