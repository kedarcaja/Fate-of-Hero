using System;
using UnityEngine;
using UnityEngine.AI;
using UnityScript.Steps;
using Random = UnityEngine.Random;

public enum Mode { stay, FollowMe, GoToWaypoint, GoToWaypointWithPlayer, WalkBetweenWaypoints }
[SelectionBase]
public class NPCController : MonoBehaviour
{
    [SerializeField]
    private float Range; // distance in scene units below which the NPC will increase speed and seek the player
    [SerializeField]
    private Mode mode;
    [SerializeField]
    private Transform target; //Set the location of the NPC
    [SerializeField]
    private Transform[] waypoints; // collection of waypoints which define a patrol area

    float patrolTime = 15; // time in seconds to wait before seeking a new patrol destination
    int index; // the current waypoint index in the waypoints array
    float speed, agentSpeed; // current agent speed and NavMeshAgent component speed
    Transform player; // reference to the player object transform
    public bool IsFollow;

    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent
    
    

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0 )
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.L)&& IsFollow)
        {
            mode = Mode.GoToWaypointWithPlayer;
            agent.speed = agentSpeed;
        }
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
                agent.speed = 0f;
                break;

            case Mode.FollowMe:
                if (player != null && Vector3.Distance(transform.position, player.position) > Range / 2)
                {
                    agent.destination = player.position;
                    agent.speed = agentSpeed;
                }
                else if (player != null && Vector3.Distance(transform.position, player.position) <= Range / 2)
                {
                    agent.speed = 0;
                }
                break;

            case Mode.GoToWaypoint:
                agent.destination = target.position;
                agent.speed = agentSpeed / 2;
                break;

            case Mode.GoToWaypointWithPlayer:
                agent.destination = target.position;
                agent.speed = agentSpeed;

                if (player != null && Vector3.Distance(transform.position, player.position) > Range)
                {
                    agent.speed = 0f;
                }
                break;

            case Mode.WalkBetweenWaypoints:
                agent.destination = waypoints[index].position;
                agent.speed = agentSpeed / 2;
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
        this.mode =(Mode) Enum.Parse(typeof(Mode),mode);
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}
