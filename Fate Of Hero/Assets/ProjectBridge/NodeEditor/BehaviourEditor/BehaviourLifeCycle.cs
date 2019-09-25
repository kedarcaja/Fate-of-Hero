using NodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BehaviourEditor
{
    [Serializable]
    public class BehaviourLifeCycle : NodeGraphLifeCycle
    {
        public override void CheckAlwaysConditions()
        {
            foreach (BaseNode b in graph.nodes)
            {
                if (b.drawNode is CheckAlwaysNode)
                {
                    foreach (ECondition c in b.alwaysCheckConditions)
                    {
                        if (ConditionNode.IsChecked(c, (graph as BehaviourGraph).character)) currentNode = b;
                    }
                }
            }
        }
        public override void DecideForNextNode()
        {
            if (currentNode.drawNode is ConditionNode)
            {


                if (ConditionNode.IsChecked(currentNode.condition, (graph as BehaviourGraph).character))
                {
                    if (currentNode.transitions.Exists(x => x.Value == "true"))
                    {

                        currentNode = currentNode.transitions.Find(x => x.Value == "true").endNode;


                    }
                }
                else
                {
                    if (currentNode.transitions.Exists(x => x.Value == "false"))
                    {

                        currentNode = currentNode.transitions.Find(x => x.Value == "false").endNode;

                    }
                }


                return;
            }
            base.DecideForNextNode();

        }


    }
}