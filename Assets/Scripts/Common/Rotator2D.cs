using UnityEngine;
using System.Collections;

public class Rotator2D : MonoBehaviour
{

    public enum DirectionType : byte
    {
        Forward,
        Backward
    }

    public DirectionType Direction = DirectionType.Forward;
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Direction == DirectionType.Forward ? Vector3.back : Vector3.forward, Time.deltaTime * Speed);
    }
}
