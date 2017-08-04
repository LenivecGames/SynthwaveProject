using UnityEngine;
using System.Collections;


public class Visible : MonoBehaviour
{


    protected void OnBecameVisible()
    {
        enabled = true;
    }

    protected void OnBecameInvisible()
    {
        enabled = false;
    }
}
