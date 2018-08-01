using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	private Camera cam;
	[SerializeField]
	private float maxZoom,minZoom;
	void Start () {
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void CameraZoom()
	{
		if(cam.orthographicSize<maxZoom)
		cam.orthographicSize += 2;
	}
	public void CameraUnZoom()
	{
		if(cam.orthographicSize>minZoom)
		cam.orthographicSize -= 2;
	}
}
