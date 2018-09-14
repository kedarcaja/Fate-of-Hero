using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private float X, Y, Z = 0;
	
	void Update () {

        transform.position = new Vector3(gameObject.transform.position.x+X, gameObject.transform.position.y+Y, gameObject.transform.position.z+Z);
	}
}
