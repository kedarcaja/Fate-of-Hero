using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
namespace FourGames
{
    public class EnemyScript : EntityScript
    {
       

        protected override void Update()
        {
            if (IsAlive() && GetDistanceFrom(FindObjectOfType<PlayerScript>().transform.position) <= 20 && FindObjectOfType<PlayerScript>().IsAlive())
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
      
        public void Attack()
        {
            anim.SetBool("Attack",true);
        }
    }
}