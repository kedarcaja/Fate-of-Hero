//using BehaviourEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FourGames
{
    public abstract class EntityScript : CharacterScript
    {
        //  public BehaviourGraph currentGraph;

        public void InitGraph()
        {
            /*  if (currentGraph)
              {
                  currentGraph.LiveCycle = new BehaviourLifeCycle();
                  currentGraph.LiveCycle.Init(currentGraph);
                  currentGraph.character = this;
              }*/
        }
        protected override void Awake()
        {
            //  InitGraph();
            base.Awake();
        }
        protected override void Update()
        {
            base.Update();
            //   if (currentGraph != null)
            {
                //      currentGraph.LiveCycle.Tick();
            }
        }

        /*  public Vector3 GetRandomMoveArea(RandomMoveArea area)
          {
              if (AgentReachedTarget())
              {
                  Vector3 randomDirection = Random.insideUnitSphere * area.radius;
                  randomDirection += area.transform.position;
                  NavMeshHit hit;
                  NavMesh.SamplePosition(randomDirection, out hit, area.radius, 1);
                  Vector3 finalPosition = hit.position;
                  return finalPosition;
              }
              return agent.destination;
          }*/
        /*  public void RandomMove(RandomMoveArea area)
          {
              SetDestination(GetRandomMoveArea(area));
          }
          */
        /*    public bool PlayerIsClose()
            {
                return ObjectIsClose(PlayerScript.Instance.transform, characterData.InteractionRadius);
            }
            */

  
    }
}
