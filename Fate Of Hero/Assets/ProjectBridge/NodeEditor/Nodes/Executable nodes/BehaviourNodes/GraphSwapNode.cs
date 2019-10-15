
using BehaviourEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Graph Swap")]
    public class GraphSwapNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR
            b.swapGraph = EditorGUILayout.ObjectField(b.swapGraph, typeof(BehaviourGraph), false) as BehaviourGraph;
#endif
        }

        public override void Execute(BaseNode b)
        {
           // b.BehaviourGraph.character.currentGraph = b.swapGraph;
            b.BehaviourGraph.character.InitGraph();
            b.nodeCompleted = true;
        }
    }
}