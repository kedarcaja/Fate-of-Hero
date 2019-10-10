
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
	public float rotateSpeed;

	Vector3 desiredDirection;
	private float inputX = 0;
	private float inputZ = 0;
	public float maxSpeed = 5;
	public float minSpeed = 2.5f;
	[SerializeField]
	private float animationCrossSpeed = 1;

    public static PlayerScript Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Instance = FindObjectOfType<PlayerScript>();

    }
    protected override void Update()
	{


		if (Input.GetKey(KeyCode.LeftShift))
		{
			agent.speed = Mathf.Lerp(agent.speed, maxSpeed, Time.deltaTime * animationCrossSpeed);

			IsRunning = true;
		}
		else
		{
			if (agent.speed > minSpeed && agent.velocity != Vector3.zero)
			{
				agent.speed = Mathf.Lerp(agent.speed, minSpeed, Time.deltaTime * animationCrossSpeed);
			}
			IsRunning = false;

		}


		Move();
		base.Update();
	}


	public void Move()
	{
		inputX = Input.GetAxis("Horizontal");
		inputZ = Input.GetAxis("Vertical");

		Vector3 forward = Camera.main.transform.forward;
		Vector3 right = Camera.main.transform.right;
		forward.y = 0;
		right.y = 0;
		forward.Normalize();
		right.Normalize();
		Vector3 v = new Vector3(inputX, inputZ);


		desiredDirection = inputZ * forward + inputX * right;

		agent.velocity = desiredDirection * agent.speed; ;

		if (v != Vector3.zero)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredDirection), rotateSpeed*Time.deltaTime);
		}

	}
}

