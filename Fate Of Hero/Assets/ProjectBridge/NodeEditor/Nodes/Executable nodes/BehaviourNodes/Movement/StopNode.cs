using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Stop Move")]

    public class StopNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {

            GUIStyle s = new GUIStyle();
            s.fontSize = 30;
            s.richText = true;
            s.margin.left = 75;
#if UNITY_EDITOR
            BehaviourEditor.GetEGLLable("Stop", s);
#endif
        }

        public override void Execute(BaseNode b)
        {
            b.BehaviourGraph.character.Agent.isStopped = true;
            b.BehaviourGraph.character.Agent.ResetPath();
            b.BehaviourGraph.character.Agent.isStopped = false;

            b.nodeCompleted =  b.BehaviourGraph.character.Agent.velocity.magnitude == 0;
        }
    }
}
