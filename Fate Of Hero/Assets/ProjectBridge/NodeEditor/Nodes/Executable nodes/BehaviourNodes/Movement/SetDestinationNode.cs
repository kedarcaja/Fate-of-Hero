using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/SetDestination")]
    public class SetDestinationNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR

            BehaviourEditor.GetEGLLable("target: ", GUIStyle.none);


            b.destinationTargetName = EditorGUILayout.TextField(b.destinationTargetName);
#endif
        }

        public override void Execute(BaseNode b)
        {
            if (b.destinationTarget == null)
            {

                foreach (GameObject g in GameObject.FindObjectsOfType(typeof(GameObject)))
                {

                    if (g.name == b.destinationTargetName)
                    {
                        b.destinationTarget = g.transform;

                    }
                }

            }
            else
            {
                b.BehaviourGraph.character?.SetTarget(b.destinationTarget);
                b.nodeCompleted = b.BehaviourGraph.character.AgentReachedTarget();
            }
        }
    }
}