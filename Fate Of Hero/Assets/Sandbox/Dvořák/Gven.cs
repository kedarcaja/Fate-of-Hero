using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gven : MonoBehaviour {
	private NavMeshAgent agent;
	[SerializeField]
	private Transform portStart,portEnd,bridge,player,currentTarget;
	private int currentIndex;
	private bool startPort,agentFollow;
	
	
	
	void Start () {
		agent = GetComponent<NavMeshAgent>();

		startPort = false;
		agentFollow = true;
		currentTarget = bridge;
	}
	
	void Update () {

		if (agentFollow)
		{
			agent.SetDestination(currentTarget.position);
		}
		if (startPort)
		{
			transform.position = portEnd.position;
		}
	
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform == bridge)
		{
			currentTarget = portStart;
		}
		if (other.transform == portStart)
		{
			agent.enabled = false;
			startPort = true;
		}
		if (other.transform == portEnd)
		{
			startPort = false;
			currentTarget = player;
			agent.enabled = true;
			agentFollow = true;
		}
	}
	

}
