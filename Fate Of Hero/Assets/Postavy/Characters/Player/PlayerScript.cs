
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FourGames
{
    public class PlayerScript : CharacterScript
    {
        [Space]
        [Header("Player")]
        [SerializeField]
        private float rotateSpeed;
        Vector3 desiredDirection;
        private float inputX = 0;
        private float inputZ = 0;
        [SerializeField]
        private SkinnedMeshRenderer swordRender;
        [SerializeField]
        private float animationCrossSpeed = 1;
       
        public static PlayerScript Instance { get; private set; }
        public SkinnedMeshRenderer SwordRender => swordRender; 

        protected override void Awake()
        {
            base.Awake();
            Instance = FindObjectOfType<PlayerScript>();

        }
        protected override void Update()
        {
            if (!IsAlive()) return;


            HandleMove();
            HandleAttack();

            base.Update();
        }
      

        public void HandleMove()
        {
            if (agent.isStopped || !agent.enabled) return;


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

            agent.velocity = desiredDirection.normalized * agent.speed; ;

            if (v != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), rotateSpeed);
            }

        }
        public void HandleAttack()
        {
            if (!CanAttack) return;

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetMouseButtonDown(0))
            {

                DisableMove();
                transform.rotation = Camera.main.transform.rotation;
                agent.velocity = Vector3.zero;
                anim.CrossFade("Attack1", 0.1f);
                CanAttack = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetMouseButtonDown(1) )
            {
                DisableMove();
                transform.rotation = Camera.main.transform.rotation;
                agent.velocity = Vector3.zero;
                anim.CrossFade("Attack2", 0.1f);
                CanAttack = false;
            }
           
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Default"))
            {
                EnableAttack();
            }
        }

        public void Heal(float Hp)
        {
            characterData.Health += Hp;
            UpdateHealth();
        }
    }
}