using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float cameraMoveSpeed = 120.0f;
	public GameObject target;
	public float clampAngel = 80f;
	public float InputSensitivityX = 150f, InputSensitivityY = 150f;
	private float mouseX, mouseY;
	private float finalInputX, finalInputZ;
	private float rotX, rotY;


	private void Start()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotX = rot.x;
		rotY = rot.y;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

	}
	private void Update()
	{
		//float inputX = Input.GetAxis("RightStickHorizontal");
		//float inputZ = Input.GetAxis("RightStickVertical");
		mouseX = Input.GetAxis("Mouse Y");
		mouseY = Input.GetAxis("Mouse X");

		finalInputX = /*inputX*/  mouseX;
		finalInputZ = /*inputZ*/mouseY;
		rotY += finalInputZ * InputSensitivityX * Time.deltaTime;
		rotX += finalInputX * InputSensitivityY * Time.deltaTime;

		rotX = Mathf.Clamp(rotX, -clampAngel, clampAngel);
		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.rotation = localRotation;



	}
	private void LateUpdate()
	{
		CameraUpdater();
	}
	void CameraUpdater()
	{
		float step = cameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
	}

}
