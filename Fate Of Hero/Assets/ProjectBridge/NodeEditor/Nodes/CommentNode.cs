using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace NodeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Comment")]
    public class CommentNode : DrawNode
    {

        public override void DrawWindow(BaseNode b)
        {
            b.comment = GUILayout.TextArea(b.comment, 200);
        }
        public override void DrawCurve(BaseNode b)
        {

           
        }

    }
}