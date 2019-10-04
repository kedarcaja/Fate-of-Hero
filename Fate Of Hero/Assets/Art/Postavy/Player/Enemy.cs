using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Entity, IID
{
	public Equipment Head, Spaulder, Arm, Chest, Trausers, Shoes;
	public Weapon Weapon, Bow, SecondHand;

	private Vector3 lastPlayerPosition;// save
	[SerializeField]
	private float playerSearchRadius;
	[SerializeField]
	private List<FieldOfView> fieldsOfView = new List<FieldOfView>();

	[SerializeField]
	private float maxFieldOfViewAngle;
	private IncrementTimer playerSearchTimer = new IncrementTimer();
	[SerializeField]
	private int playerSearchTime = 0;
	private Vector3 startPosition; // uložení ?
	[SerializeField]
	private float maxDistanceFromPlayer;
	[SerializeField]
	private int id;
	public int ID
	{
		get
		{
			return id;
		}
	}

	protected override void Awake()
	{

		playerSearchTimer.Init(1, 1, this);
		startPosition = transform.position;
		playerSearchTimer.OnTimerStart += new TimerHandler(() => (stats as EnemyStats).State = EEnemyState.Search);
		playerSearchTimer.OnTimerEnd += new TimerHandler(() => (stats as EnemyStats).State = EEnemyState.Neutral);
		base.Awake();
		Idle();

		(stats as EnemyStats).State = EEnemyState.Neutral;
	}
	public override void Die()
	{
		Debug.Log("died");
		QuestManager.Instance.UpdateKillQuestParts(this);
		base.Die();
	}
	protected override void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			Die();
		}

		if (CanSeeTarget(PlayerScript.Instance.transform, PlayerScript.Instance.Agent.height))
		{
			/////////////////////// interaction
			QuestManager.Instance.UpdateKillQuestParts(this);

			//////////////////////
			//Attack(PlayerScript.Instance);
			lastPlayerPosition = PlayerScript.Instance.transform.position;
			(stats as EnemyStats).State = EEnemyState.Attack;
			if (playerSearchTimer.isRunning)
			{
				playerSearchTimer.Stop();
			}
		}
		else
		{
			if ((stats as EnemyStats).State == EEnemyState.Attack || (stats as EnemyStats).State == EEnemyState.Search)
			{
				(stats as EnemyStats).State = EEnemyState.Search;
				if (!playerSearchTimer.isRunning && Vector3.Distance(transform.position, lastPlayerPosition) <= Agent.stoppingDistance)
				{
					playerSearchTimer.Start();
				}
				if (playerSearchTimer.isRunning && playerSearchTimer.GetTimeInt() < playerSearchTime)
				{
					stats.TargetVector.Target = null;
					if (AgentIsOnPosition)
					{
						SetDestination(GetRandomPosition(lastPlayerPosition, playerSearchRadius));
					}
				}
				else if (playerSearchTimer.isRunning)
				{

					playerSearchTimer.Stop();
					SetDestination(startPosition);

				}

			}
			if ((stats as EnemyStats).State != EEnemyState.Search && stats.TargetVector.Destination == startPosition && AgentIsOnPosition)
			{
				(stats as EnemyStats).State = EEnemyState.Neutral;
			}

		}


		base.Update();
	}
	public override void Defend()
	{
		Idle();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="enemy">nevíme zda bude cíl jen hráč nebo i třeba nějaký jiný cíl</param>
	public override void Attack()
	{
		
	}

	/// <summary>
	/// řešeno úhlem a vzdáleností
	/// </summary>
	/// <returns></returns>
	public bool CanSeeTarget(Transform target, float targetHeight)
	{

		if ((stats as EnemyStats).State == EEnemyState.Search || (stats as EnemyStats).State == EEnemyState.Attack)
		{
			Vector3 targetDir = target.position - transform.position;
			float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

			if (!Physics.Linecast(transform.position, target.position))
			{
				if (angleToPlayer >= -maxFieldOfViewAngle && angleToPlayer <= maxFieldOfViewAngle && Vector3.Distance(transform.position, target.position) < maxDistanceFromPlayer)
				{
					Debug.DrawLine(transform.position, target.position, Color.red);
					return true;
				}
			}
		}
		else
		{
			if (fieldsOfView.Any(f => f.CanSeeTarget(target, targetHeight)))
			{
				(stats as EnemyStats).State = EEnemyState.Search;
				return true;
			}
		}
		return false;
	}


	private Vector3 GetRandomPosition(Vector3 startPosition, float rds)
	{
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * rds;
		randomDirection += startPosition;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, rds, 1);
		Vector3 finalPosition = hit.position;
		return finalPosition;
	}

	protected override void OnDrawGizmos()
	{

		if ((stats as EnemyStats).State == EEnemyState.Neutral)
		{
			fieldsOfView.ForEach(f => f.Draw());
		}
		else if ((stats as EnemyStats).State == EEnemyState.Detected)
		{
			fieldsOfView.ForEach(f => f.Draw());
		}

		base.OnDrawGizmos();
	}
 
 void ApplyDamage(float damage)
	{
		if (stats.Health > 0)
		{ 
			stats.Health -= damage;
			Debug.Log("Právě jsi mi dal hit za: "+damage+", ale zbývá mi ještě: "+stats.Health+" hp");
			if (!stats.IsAlive)
			{
				Debug.Log("<color=red>Začínám si myslet že jsem zemřel</color>");
				Die();
			}
		}
	}
}
