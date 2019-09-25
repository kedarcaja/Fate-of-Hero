using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Main/Enter")]
    public class EnterNode : DrawNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }
        public override void DrawWindow(BaseNode b)
        {
            var style = new GUIStyle("label");
            style.fontSize = 25;
            GUI.color = Color.red;
            style.margin.left = 25;
            GUILayout.Label("Enter", style);

        }
    }
}