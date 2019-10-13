
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FourGames
{
    public class PlayerScript : CharacterScript
    {
        public float rotateSpeed;

        Vector3 desiredDirection;
        private float inputX = 0;
        private float inputZ = 0;
        public float maxSpeed = 5;
        public float minSpeed = 2.5f;
        [SerializeField]
        private float animationCrossSpeed = 1;

        private bool canAttack = true;
        private bool isGrounded = true, isAttacking;

        public static PlayerScript Instance { get; private set; }

        public bool keys = true;

        public float Health = 100;
        [SerializeField]
        private Sword sword;

        protected override void Awake()
        {
            base.Awake();
            Instance = FindObjectOfType<PlayerScript>();

        }
        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                agent.speed = Mathf.Lerp(agent.speed, maxSpeed, Time.deltaTime * animationCrossSpeed);
                IsRunning = true;
            }
            else
            {
                if (agent.speed > minSpeed && agent.velocity != Vector3.zero)
                {
                    agent.speed = Mathf.Lerp(agent.speed, minSpeed, Time.deltaTime * animationCrossSpeed);
                }
                IsRunning = false;

            }

            if (isGrounded && keys)
            {
                if (!isAttacking)
                {
                    HandleJump();
                    Move();


                }
                HandleAttack();

            }

            base.Update();
        }


        public void Move()
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();
            Vector3 v = new Vector3(inputX, inputZ);


            desiredDirection = inputZ * forward + inputX * right;

            agent.velocity = desiredDirection * agent.speed; ;

            if (v != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), rotateSpeed);
            }

        }
        public void HandleAttack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && canAttack && isGrounded)
            {
                anim.CrossFade("Attack1", 0.15f);
                canAttack = false;

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && canAttack)
            {
                anim.CrossFade("Attack2", 0.15f);
                canAttack = false;
            }
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Default"))
            {
                EnableAttack();
            }
        }
        public void HandleJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                agent.enabled = false;
                anim.CrossFade("Jump", Time.deltaTime);
                isGrounded = false;
            }
        }
        public void EnableAttack()
        {
            canAttack = true;
            isAttacking = false;
        }
        public void SetGrounded()
        {
            isGrounded = true;
            agent.enabled = true;

        }
        public void TakeDamage(float Damage, Transform attacker)
        {
            if (Health > 0)
            {
                Health -= Damage;
                anim.CrossFade("Dodge", Time.deltaTime);
            }
            else
            {
                anim.CrossFade("Die", Time.deltaTime);
            }
        }

        public void EnableDamage()
        {
            sword.EnableDamage();
        }
        public void DisableDamage()
        {
            sword.DisableDamage();
        }

    }
}