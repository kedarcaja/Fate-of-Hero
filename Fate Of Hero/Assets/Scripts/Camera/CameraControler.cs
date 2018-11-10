using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    [Header("target")]
    public Transform target;
    [Header("Distance")]
    public float distance = 5f;
    public float MinDistance;
    public float MaxDistance;
    public Vector3 offset;
    [Header("Speeds")]
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 1;

    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!target)
        {
            print("Chybí kamera");
            return;
        }

        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scrollSensitivity;
        distance = Mathf.Clamp(distance,MinDistance,MaxDistance);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * distance;

        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
	}
}
