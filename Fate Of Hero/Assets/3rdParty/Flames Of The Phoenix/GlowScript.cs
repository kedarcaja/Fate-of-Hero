using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour {

	Renderer rend;
	float colInt;
	Color c;
	public float minColInt = 0.5f, maxColInt = 1f;


	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		colInt = Random.Range (minColInt, maxColInt);
		c = rend.material.color;
		c.a = colInt;
		rend.material.color = c;
	}
}
