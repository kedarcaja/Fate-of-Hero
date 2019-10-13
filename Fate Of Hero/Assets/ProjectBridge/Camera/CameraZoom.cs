using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	public float minDistance = 1f, maxDistance = 4f,sensitivity = 10f;

	void Update()
	{
        if (!Book.Instance.IsActive())
        {
            float fov = Camera.main.fieldOfView;
            fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minDistance, maxDistance);
            Camera.main.fieldOfView = fov;
        }
        
	}
}
