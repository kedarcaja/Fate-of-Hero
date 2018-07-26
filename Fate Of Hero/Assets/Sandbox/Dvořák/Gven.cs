﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gven : MonoBehaviour {
	private NavMeshAgent agent;
	[SerializeField]
	private Transform portStart,portEnd,bridge,player,currentTarget;
	private int currentIndex;
	private bool startPort,agentFollow;
	[SerializeField]
	private Subtitles dialog;
	
	
	
	void Start () {
		agent = GetComponent<NavMeshAgent>();

		startPort = false;
		agentFollow = true;
		
	}
	
	void Update () {
		if (dialog.Dialogs[0].trigger)
		{
			currentTarget = bridge;
		}
		if (agentFollow)
		{
			agent.SetDestination(currentTarget.position);
		}
		if (startPort)
		{
			transform.position = portEnd.position;
		}
	
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform == bridge&&dialog.Dialogs[0].ended)
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
			agent.stoppingDistance = 2.5f;
			agentFollow = true;
		}
	}
	

}
