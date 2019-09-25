using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace NodeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/CheckAlways")]
    public class CheckAlwaysNode : DrawNode
    {
        public override void DrawCurve(BaseNode b)
        {

        }
        public override void DrawWindow(BaseNode b)
        {
            var list = b.alwaysCheckConditions;
            b.alwaysCheckCoun = Mathf.Max(b.alwaysCheckCoun, EditorGUILayout.IntField("size", list.Count));

            if (!b.collapse)
            {
                while (b.alwaysCheckCoun < list.Count)
                    list.RemoveAt(list.Count - 1);
                while (b.alwaysCheckCoun > list.Count)
                    list.Add(ECondition.IsAlive);

                for (int i = 0; i < list.Count; i++)
                {

                    list[i] = (ECondition)EditorGUILayout.EnumPopup(list[i]);
                    EditorGUILayout.LabelField("");

                }
                b.WindowRect.height = b.drawNode.Height + 30 * list.Count;

            }

        }
    }

}