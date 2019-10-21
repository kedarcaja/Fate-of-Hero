using DialogEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeEditor
{
    [Serializable]
    public abstract class NodeGraphLifeCycle
    {
        public BaseNode currentNode;
        public BaseNode CurrentNode { get => currentNode; }


        [NonSerialized]
        public NodeGraph graph;

        public virtual void Tick()
        {
            CheckAlwaysConditions();


            DecideForNextNode();
            if (currentNode != null)
            {
                currentNode.Execute();
            }

        }
        public void CheckTransitions()
        {
            if (currentNode != null)
            {
                foreach (Transition t in currentNode.transitions)
                {
                    currentNode = t.endNode;
                    currentNode.nodeCompleted = false;
                    
                    break;
                }
            }

        }
        public abstract void CheckAlwaysConditions();
        public void Init(NodeGraph graph)
        {
            this.graph = graph;
            currentNode = graph.nodes.Find(f => (f.drawNode is EnterNode));
        }
        public virtual void DecideForNextNode()
        {

            currentNode.executed = false;

           
            if (!(currentNode.drawNode is DialogDecisionNode)&&((currentNode.drawNode is ExecutableNode && currentNode.nodeCompleted)|| currentNode.drawNode is CheckAlwaysNode || currentNode.drawNode is EnterNode))
            {
                CheckTransitions();
                return;
            }
           
            if (currentNode.drawNode is PortalNode)
            {
                BaseNode b = graph.nodes.Find(n => n.ID == currentNode.portalTargetNodeID);
                currentNode = b != null ? b : currentNode;
                return;
            }
         

        }
    }
}