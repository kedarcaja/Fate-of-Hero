using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private GameObject gameObject;
	
	void Update () {

        transform.position = new Vector3(gameObject.transform.position.x + 0.44f, gameObject.transform.position.y+12.88f, gameObject.transform.position.z-16.74f);
	}
}
