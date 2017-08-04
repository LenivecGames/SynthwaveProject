using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class MovePosition : MonoBehaviour
{

    public Vector3 Speed;

    private void Update()
    {
        transform.localPosition += Speed * Time.deltaTime;
    }
}
