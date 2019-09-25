using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
	public float minDistance = 1f, maxDistance = 4f, smooth = 10f, distance;
	Vector3 dollyDir;
	public Vector3 dollyDirAdjusted;

	void Start()
	{
		dollyDir = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;

	}

	// Update is called once per frame
	void Update()
	{

		Vector3 dirCamPos = transform.parent.TransformPoint(dollyDir * maxDistance);
		RaycastHit hit;


		if (Physics.Linecast(transform.parent.position, dirCamPos, out hit))
		{
			distance = Mathf.Clamp(hit.distance * 0.9f, minDistance, maxDistance);
		}
		else
		{
			distance = maxDistance;
		}
		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
