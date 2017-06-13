using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timescale : MonoBehaviour {
	public float time = 1;
	// Use this for initialization
	void Start () {
		Time.timeScale = time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
