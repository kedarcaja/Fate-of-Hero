using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Character
{
    Transform player;
    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area
    int index; // the current waypoint index in the waypoints array
    public float patrolTime = 15; // time in seconds to wait before seeking a new patrol destination

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    void Tick()
    {
        MyAgent.destination = waypoints[index].position;
        MyAgent.speed = AgentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            MyAgent.destination = player.position;
            MyAgent.speed = AgentSpeed;

        }
        if (player != null && Vector3.Distance(transform.position, player.position) < 2)
        {
            MyAnimator.SetBool("attack", true);
        }
        else
        {
            MyAnimator.SetBool("attack", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
