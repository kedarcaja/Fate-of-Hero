using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour {

   
    private float speed;
    private float agentSpeed;

    [SerializeField]
    protected Stats Health;

    public Stats MyHealth
    {
        get
        {
            return Health;
        }
    }

    public Animator MyAnimator { get; set; }
    private NavMeshAgent agent;
    public NavMeshAgent MyAgent
    {
        get
        {
            return agent;
        }

        set
        {
            agent = value;
        }
    }
    private Rigidbody myrigidbody;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return Health.CurrentVal > 0;

        }
    }

    public float AgentSpeed
    {
        get
        {
            return agentSpeed;
        }

        set
        {
            agentSpeed = value;
        }
    }

    protected virtual void Awake()
    {
        if (Health.Bar!=null)
        {
            Health.Initialize();
        }
        
        if (agent != null) { agentSpeed = agent.speed; }
        myrigidbody = GetComponent<Rigidbody>();
        MyAnimator = GetComponent<Animator>();
        MyAgent = GetComponent<NavMeshAgent>();
    }
    

    protected virtual void Move()
    {
            MyAnimator.SetFloat("Speed", MyAgent.velocity.magnitude);
    }
}
