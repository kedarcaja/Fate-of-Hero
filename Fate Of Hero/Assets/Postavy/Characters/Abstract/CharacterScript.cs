using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
using Data;
//using NodeEditor;
namespace FourGames
{

    public class CharacterScript : MonoBehaviour
    {
        [Header("Character")]
        protected NavMeshAgent agent;
       
        protected Rigidbody rigid;
        protected Animator anim;
        protected bool IsRunning = false;
        [SerializeField]
        protected Character characterData;
        [SerializeField]
        protected float maxSpeed = 6;
        [SerializeField]
        protected float minSpeed = 3;
        [SerializeField]
        protected Stats healthBar;

        public NavMeshAgent Agent { get => agent; }
        public Character CharacterData { get => characterData; }
        public Animator Animator { get => anim; }


        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>();
        }

        private void UpdateHealth()
        {
            if (healthBar != null) 
            {
                healthBar.CurrentVal = characterData.Health;
            }
        }

        protected virtual void Update()
        {
            anim.SetFloat("magnitudeSpeed", agent.velocity.magnitude);
        }
        public bool AgentReachedTarget()
        {
            float dist = agent.remainingDistance;
            return (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance) && agent.velocity == Vector3.zero ;
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
        public virtual void TakeDamage(float Damage, Transform attacker)
        {
            characterData.Health -= Damage;
            if (IsAlive())
            {
                UpdateHealth();
                anim.CrossFade("Dodge", Time.deltaTime);
            }
            else
            {
                Destroy(anim);
                Destroy(agent);
                Destroy(GetComponent<Collider>());

                for (int i = 0; i < transform.childCount; i++)
                {
                    //destroys ragdoll colliders
                    Destroy(transform.GetChild(i).GetComponent<Collider>());
                }
            }
        }
        public virtual bool IsAlive()
        {
            return characterData.IsAlive;
        }
    }
}