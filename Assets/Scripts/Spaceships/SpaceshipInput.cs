using UnityEngine;
using System.Collections;
using Lean.Touch;

namespace NeonSpace
{
    [RequireComponent(typeof(Spaceship))]
    public class SpaceshipInput : MonoBehaviour
    {
        private Spaceship _Spaceship;
        private Camera _MainCamera;
        // Use this for initialization
        void Start()
        {
            _Spaceship = GetComponent<Spaceship>();
            _MainCamera = Camera.main;

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
                        if (touch.position.x < Screen.width / 2)
                        {
                            Vector2 touchInWorld = _MainCamera.ScreenToWorldPoint(touch.position);
                            if (touchInWorld.y > transform.position.y)
                            {
                                _Spaceship.MoveUp();
                            }
                            else if (touchInWorld.y < transform.position.y)
                            {
                                _Spaceship.MoveDown();
                            }
                            else
                            {
                                _Spaceship.MoveForward();
                            }
                        }
                    }
                }
                else if (SystemInfo.deviceType == DeviceType.Desktop)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (Input.mousePosition.x < Screen.width / 2)
                        {
                            Vector2 pos = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
                            if (pos.y != transform.position.y)
                            {
                                if (pos.y > transform.position.y + 0.15f)
                                {
                                    _Spaceship.MoveUp();
                                }
                                else if (pos.y < transform.position.y + 0.15f)
                                {
                                    _Spaceship.MoveDown();
                                }
                                else
                                {
                                    _Spaceship.MoveForward();
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
