using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;

public delegate void TimerEventHandler();
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
	[SerializeField]
	protected CharacterStats stats;
	public CharacterStats Stats { get { return stats; } }
	protected Animator anim;
	[ExecuteAlways]
	public NavMeshAgent Agent { get { return GetComponent<NavMeshAgent>(); } }
	protected Rigidbody rigid;
	public bool AgentIsOnPosition
	{
		get
		{
			Agent.isStopped = true;
			anim.SetFloat("speed",0);
			return Agent.remainingDistance <= Agent.stoppingDistance && Agent.pathStatus == NavMeshPathStatus.PathComplete;
		}
	}
	protected IncrementTimer idleTimer = new IncrementTimer(), walkTimer = new IncrementTimer(), runTimer = new IncrementTimer();
	[SerializeField]
	protected float interactionRadius, collisionRadius;
	public float InteractionRadius { get { return interactionRadius; } }
	public float CollisionRadius { get { return collisionRadius; } }
	public bool AgentAvailable { get; set; }
	protected virtual void Awake()
	{
		AgentAvailable = true;

		ResetDestination();

		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();


		walkTimer.OnTimerUpdate += new TimerHandler(RestoreStamina);
		idleTimer.OnTimerUpdate += new TimerHandler(RestoreStamina);

		runTimer.OnTimerUpdate += new TimerHandler(delegate { stats.Stamina -= CharacterStats.KEDAR + runTimer.GetTimeInt() * 0.5f; });

		walkTimer.OnTimerStart += delegate
		{
			idleTimer.Stop();
			runTimer.Stop();
		};
		idleTimer.OnTimerStart += delegate
		{
			walkTimer.Stop();
			runTimer.Stop();
		};
		runTimer.OnTimerStart += delegate
		{
			walkTimer.Stop();
			idleTimer.Stop();
		};



		idleTimer.Init(1, 2, this);
		walkTimer.Init(1, 4, this);
		runTimer.Init(1, 2, this);


	}
	protected virtual void Update()
	{
		Move();
		//Debug.Log("Walk: " + walkTimer.isRunning + " Run: " + runTimer.isRunning + " Idle: " + idleTimer.isRunning);// kontrola spouštění časovačů

	}

	public void Move()
	{
		
		anim.SetFloat("speed", Agent.velocity.magnitude);
		Agent.SetDestination(stats.TargetVector.Destination);
	
	}


	public void Idle()
	{

	
		Agent.isStopped = true;

		if (!idleTimer.isRunning)
		{
			idleTimer.Start();
		}

	}
	public void Walk()
	{

		Agent.isStopped = false;
		

		if (!walkTimer.isRunning)
		{
			walkTimer.Start();
		}
		Agent.speed = 2; // do statů dát walk speed = walkSpeed
	}
	public void Run()
	{
		

		Agent.isStopped = false;
		
		if (stats.Stamina > 0)
		{

			if (!runTimer.isRunning)
			{
				runTimer.Start();
			}

			Agent.speed = 6;
		}
		else
		{
			runTimer.Stop();
			Walk();
		}
	}
	public void SetDestination(Vector3 trg)
	{
		stats.TargetVector.Destination = trg;

	}
	public void SetTarget(Transform trg)
	{
		stats.TargetVector.Target = trg;
		if (stats.TargetVector.Target != null && stats.TargetVector.Target.GetComponent<Character>() != null)
		{
			stats.TargetVector.Target.GetComponent<Character>().stats.Followers.Remove(this);
		}
		if (trg != null)
		{
			SetDestination(trg.position);
			if (trg.GetComponent<Character>() != null)
			{
				trg.GetComponent<Character>().stats.Followers.Add(this);
			}
		}
	}
	protected void RestoreStamina()
	{
		stats.Stamina += 10;
	}
	public void DisableAgent()
	{
		Agent.speed = 0;
		Agent.velocity = Vector3.zero;
		anim.SetFloat("speed", 0);
		AgentAvailable = false;
		Agent.isStopped = true;
		ResetDestination();

	}
	public void RestoreAgent()
	{

		AgentAvailable = true;
		Agent.isStopped = false;
	}
	public void ResetDestination()
	{
		SetDestination(Vector3.zero);
		SetTarget(null);
	}
	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		//interaction radius
		Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), InteractionRadius);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, collisionRadius);
	}

	public bool TargetInRange(Transform target, float radius)
	{
		return Vector3.Distance(transform.position, target.position) < radius;
	}
	public bool DestinationInRange(Vector3 dest, float radius)
	{
		return Vector3.Distance(transform.position, dest) < radius;
	}

}
public class IncrementTimer
{
	public event TimerHandler OnTimerStart, OnTimerEnd, OnTimerUpdate;
	[SerializeField]
	private float updateValue = 0, time = 0;
	private float delay;
	private MonoBehaviour starter;
	public bool isRunning { get; private set; }
	public void Init(float updateValue, float delay, MonoBehaviour coroutineStarter)
	{
		starter = coroutineStarter;
		this.updateValue = updateValue;
		this.delay = delay;
		isRunning = false;
	}
	public void Start()
	{
		time = 0;
		isRunning = true;
		if (OnTimerStart != null)
		{
			OnTimerStart();
		}

		starter.StartCoroutine(Update());

	}
	protected void Reset()
	{
		Stop();
		isRunning = true;
		if (OnTimerStart != null)
		{
			OnTimerStart();
		}

		starter.StartCoroutine(Update());
	}
	protected IEnumerator Update()
	{

		if (isRunning)
		{

			while (isRunning)
			{

				yield return new WaitForSeconds(delay); //  přičítat 1 za delší čas ?
				time += updateValue;

				if (OnTimerUpdate != null)
				{
					OnTimerUpdate();
				}
				Reset();
			}
		}

	}
	public void Stop()
	{
		if (isRunning)
		{
			if (OnTimerEnd != null)
			{
				OnTimerEnd();
			}
			isRunning = false;

			starter.StopAllCoroutines();

		}
	}
	public int GetTimeInt()
	{
		return (int)time;
	}
	public float GetTimeFloat()
	{
		return time;
	}

}
public delegate void TimerHandler();
[Serializable]
public struct TargetVector
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private Vector3 destination;

	public Vector3 Destination
	{
		get
		{
			if (target == null)
			{
				return destination;
			}

			return target.position;

		}

		set
		{

			destination = value;

		}
	}

	public Transform Target
	{
		get
		{
			return target;
		}

		set
		{
			target = value;
		}
	}
}