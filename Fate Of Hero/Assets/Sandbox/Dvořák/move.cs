using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
	public float speed;
	private Vector3 direction = Vector3.zero;
	private Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		GetInput();
	}
	private void FixedUpdate()
	{
		rb.velocity = direction * speed * Time.deltaTime;
	}
	private void GetInput()
	{
		direction = Vector3.zero;
		if (Input.GetKey(KeyCode.A))
		{
			direction = Vector3.left;
		}
		if (Input.GetKey(KeyCode.D))
		{
			direction = Vector3.right;
		}
		if (Input.GetKey(KeyCode.S))
		{
			direction = Vector3.back;
		}
		if (Input.GetKey(KeyCode.W))
		{
			direction = Vector3.forward;
		}
	}
}
