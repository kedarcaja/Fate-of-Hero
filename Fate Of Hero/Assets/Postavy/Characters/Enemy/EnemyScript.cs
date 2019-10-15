using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGames
{
    public class EnemyScript : EntityScript
    {
        [SerializeField]
        private Enemy enemyData;
        public override bool IsAlive()
        {
            return enemyData.IsOneHitEnemy ? anim.enabled == true : base.IsAlive();
        }
        protected override void Update()
        {
            if (IsAlive())
            {
                if (AgentReachedTarget() )
                {
                    Attack();
                }
                float dist = GetDistanceFrom(FindObjectOfType<PlayerScript>().transform.position);
                if (dist > agent.stoppingDistance)
                {
                    anim.SetBool("Attack", false);
                    SetTarget(FindObjectOfType<PlayerScript>().transform);

                    if(dist > 3)
                    {
                        agent.speed = maxSpeed;
                    }
                    else
                    {
                        agent.speed = minSpeed;
                    }

                }
             
                base.Update();

            }

        }
        public override void TakeDamage(float Damage, Transform attacker)
        {
            if (enemyData.IsOneHitEnemy)
            {
                anim.enabled = false;
            }
            else
            {
                base.TakeDamage(Damage, attacker);
            }
        }
        public void Attack()
        {
            anim.SetBool("Attack",true);
        }
    }
}