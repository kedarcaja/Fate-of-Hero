using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityScript.Steps;
using Random = UnityEngine.Random;

public enum Mode { stay, FollowMe, GoToWaypoint, WalkBetweenWaypoints }
public delegate void HealthChange(float health);
public delegate void characterRemove();
[SelectionBase]
public class NPCScript : Character {

    
    [SerializeField]
    private float range; // distance in scene units below which the NPC will increase speed and seek the player
    [SerializeField]
    private Mode mode;
    [SerializeField]
    private Transform target; //Set the location of the NPC
    [SerializeField]
    private Transform[] waypoints; // collection of waypoints which define a patrol area

    float patrolTime = 15; // time in seconds to wait before seeking a new patrol destination
    int index; // the current waypoint index in the waypoints array
     
    Transform player; // reference to the player object transform    
   
    

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

   
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
    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }
    void Tick()
    {
        switch (mode)
        {
            case Mode.stay:


                MyAgent.speed = 0f;
                break;

            case Mode.FollowMe:
                if (player != null && Vector3.Distance(transform.position, player.position) > Range / 2)
                {
                    MyAgent.destination = player.position;
                    MyAgent.speed = AgentSpeed;
                }
                else if (player != null && Vector3.Distance(transform.position, player.position) <= Range / 2)
                {
                    MyAgent.speed = 0;
                }
                break;

            case Mode.GoToWaypoint:
                if (player != null && target != null)
                {
                    AgentSpeed = 5;
                    MyAgent.destination = target.position;
                    MyAgent.speed = AgentSpeed / 2;
                }
                break;

            case Mode.WalkBetweenWaypoints:
                if (player != null && waypoints.Length > 0)
                {
                    MyAgent.destination = waypoints[index].position;
                    MyAgent.speed = AgentSpeed / 2;
                }

                break;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
    public void SwapMode(string mode)
    {
        this.mode = (Mode)Enum.Parse(typeof(Mode), mode);
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
