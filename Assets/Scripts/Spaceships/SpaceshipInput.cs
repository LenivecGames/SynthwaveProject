using UnityEngine;
using System.Collections;
using Lean.Touch;

namespace NeonSpace
{
    [RequireComponent(typeof(Spaceship))]
    public class SpaceshipInput : MonoBehaviour
    {
        private Spaceship _Spaceship;
        // Use this for initialization
        void Start()
        {
            _Spaceship = GetComponent<Spaceship>();
            
        }

        private void Update()
        {
            if (GameManager.CurrentGameState == GameState.Playing)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _Spaceship.Shoot(1);
                }

                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.position.x > Screen.width / 2)
                        {
                            _Spaceship.Shoot(1);
                        }
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (GameManager.CurrentGameState == GameState.Playing)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    _Spaceship.MoveUp();
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    _Spaceship.MoveDown();
                }
                else
                {
                    _Spaceship.MoveForward();
                }


                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
                        {
                            _Spaceship.MoveUp();
                        }
                        else if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
                        {
                            _Spaceship.MoveDown();
                        }
                    }
                }
            }
        }
    }
}
