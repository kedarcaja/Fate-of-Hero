using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveToPoint : MonoBehaviour {
    public Transform[] Point;
    public CameraControler controler;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controler.target = Point[0];
        }
	}
}
