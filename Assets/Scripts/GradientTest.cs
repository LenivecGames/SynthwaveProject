using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;

public class GradientTest : MonoBehaviour {

    public Gradient Gradient;
    public List<Color> Colors = new List<Color>();
    public Color Color;

	// Use this for initialization
	void Start () {
        foreach(GradientColorKey colorKey in Gradient.colorKeys)
        {
            Debug.Log(colorKey.color.ToString());
            Colors.Add(colorKey.color);
        }
        Color = Gradient.Evaluate(0.2f);
        
        
	}

    private void FixedUpdate()
    {

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
