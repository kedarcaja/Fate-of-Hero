using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
using NodeEditor;

[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Rigidbody))]
public class CharacterScript : MonoBehaviour
{


    protected NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    protected Rigidbody rigid;
    protected Animator anim;
    public Animator Animator { get => anim; }
    public bool IsRunning = false;
    [SerializeField]
    protected Character characterData;
    public Character CharacterData { get => characterData; }
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }


    protected virtual void Update()
    {
        anim.SetFloat("magnitudeSpeed", agent.velocity.magnitude);

    }
    public bool AgentReachedTarget()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }


    public float GetDistanceFrom(Transform target)
    {
        return GetDistanceFrom(target.position);
    }
    public float GetDistanceFrom(Vector3 place)
    {
        return Vector3.Distance(transform.position, place);
    }
    public bool ObjectIsClose(Transform target, float closeRadius)
    {
        return PlaceIsClose(target.position, closeRadius);
    }
    public bool PlaceIsClose(Vector3 target, float closeRadius)
    {
        return GetDistanceFrom(target) <= closeRadius;
    }
    public void SetTarget(Transform target)
    {
        SetDestination(target.position);
    }
    public void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
    }
}