using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{

    public float Time = 1f;

    protected void Start()
    {
        Destroy(gameObject, Time);
    }
}
