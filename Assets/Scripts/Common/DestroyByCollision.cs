using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DestroyByCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
