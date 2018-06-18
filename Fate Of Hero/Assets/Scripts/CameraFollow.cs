using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private GameObject gameObject;
	
	void Update () {

        transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+18f, gameObject.transform.position.z-23f);
	}
}
