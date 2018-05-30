using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject gameObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(gameObject.transform.position.x + 0.44f, gameObject.transform.position.y+5f, gameObject.transform.position.z);
	}
}
