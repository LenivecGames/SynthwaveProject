using UnityEngine;
using System.Collections;

public class TextureOffset : MonoBehaviour
{

    public float ScrollSpeed = 0.90f;
    public float ScrollSpeed2 = 0.90f;

    private Renderer _Renderer;

    private void Start()
    {
        _Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {

        float offset = Time.time * ScrollSpeed;
        float offset2 = Time.time * ScrollSpeed2;
        _Renderer.material.mainTextureOffset = new Vector2(offset2, 0);
    }
}
