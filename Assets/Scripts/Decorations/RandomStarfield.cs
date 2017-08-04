using UnityEngine;
using System.Collections;

namespace NeonSpace.Decorations
{
    [RequireComponent(typeof(SGT_Starfield))]
    public class RandomStarfield : MonoBehaviour
    {
        private SGT_Starfield _Starfield;
        // Use this for initialization
        void Start()
        {
            _Starfield = GetComponent<SGT_Starfield>();
            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);


        }


        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState == GameState.Playing && (GameManager.PreviousGameState == GameState.Menu || gameStateMessage.GameState == GameState.GameOver))
            {
                _Starfield.StarfieldSeed = Random.Range(0, int.MaxValue);
                
            }
        }
    }
}
