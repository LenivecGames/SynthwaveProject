using UnityEngine;
using System.Collections;

namespace NeonSpace
{
    [DisallowMultipleComponent, RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {

        private Camera _Camera;
        [SerializeField]
        //private float _Countdown = 3f;

        private void Awake()
        {
            _Camera = GetComponent<Camera>();
            _Camera.orthographic = true;

            EventManager.Subscribe<GameStateMessage>(OnGameStateHandler);
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (GameManager.CurrentGameState == GameState.Menu)
            {
                    _Camera.orthographicSize = Mathf.Lerp(_Camera.orthographicSize, 2, 0.1f);
                    transform.localPosition = new Vector2(Mathf.Lerp(transform.localPosition.x, 0, 0.01f), 0);
            }
            else if (GameManager.CurrentGameState == GameState.Playing)
            {
                    _Camera.orthographicSize = Mathf.Lerp(_Camera.orthographicSize, 5, 0.01f);
                    transform.localPosition = new Vector2(Mathf.Lerp(transform.localPosition.x, 4, 0.01f), 0);
            }
        }

        private void OnGameStateHandler(GameStateMessage gameStateMessage)
        {

        }

        private IEnumerator ZoomInCamera()
        {

            yield return null;
        }

        private IEnumerator ZoomOutCamera()
        {

            yield return null;
        }
    }
}