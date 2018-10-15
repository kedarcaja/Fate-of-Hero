using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

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
    public NavMeshAgent Agent
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

    protected virtual void Awake()
    {
        Health.Initialize();
    }
    protected virtual void Start()
    {
        myrigidbody = GetComponent<Rigidbody>();
        MyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        
    }

    public void Move()
    {
        
            MyAnimator.SetFloat("Speed", Agent.velocity.magnitude);
        

    }
}
