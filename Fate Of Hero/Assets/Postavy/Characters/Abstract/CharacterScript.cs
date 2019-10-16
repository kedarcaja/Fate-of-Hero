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
        public  Stats healthBar;


        [SerializeField]
        protected Sword sword;

        public NavMeshAgent Agent { get => agent; }
        public Character CharacterData { get => characterData; }
        public Animator Animator { get => anim; }

        protected bool canAttack = true, canMove = true;

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            rigid = GetComponent<Rigidbody>();


            if(name == "Leo")
            {
                if(FindObjectOfType<Canvas>() !=null)
                healthBar.bar = GameObject.Find("HealthBar").GetComponent<BarScript>();

                characterData.Health = 1000;
            }
            else
            {
                characterData.Health = 50;

            }

        }
        private void Start()
        {
            if (healthBar != null)
            {
                healthBar.MaxVal = characterData.MaxHealth;
                healthBar.CurrentVal = characterData.MaxHealth;
                healthBar.Initialize();
            }
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
            if (!IsAlive()) return;
            anim.SetFloat("magnitudeSpeed", agent.velocity.magnitude);
        }
        public bool AgentReachedTarget()
        {
            float dist = agent.remainingDistance;
            return (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance) && agent.velocity == Vector3.zero;
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
            UpdateHealth();

            if (IsAlive())
            {
                anim.CrossFade("Dodge", Time.deltaTime);
            }
            else
            {
                if (this is PlayerScript)
                {
                    anim.CrossFade("Die", Time.deltaTime);
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
        }
        public virtual bool IsAlive()
        {
            return characterData.IsAlive;
        }
        public void EnableAttack()
        {
            canAttack = true;

        }
        public void DisableAttack()
        {
            canAttack = false;
            DisableMove();

        }
        public void SetGrounded()
        {
            EnableMove();
        }

        public void EnableDamage()
        {
            sword.EnableDamage();
        }
        public void DisableDamage()
        {
            sword.DisableDamage();
        }

        public void DisableMove()
        {
            agent.updatePosition = false;
            agent.isStopped = true;

        }
        public void EnableMove()
        {
            agent.isStopped = false;
            agent.updatePosition = true;
        }
    }
}