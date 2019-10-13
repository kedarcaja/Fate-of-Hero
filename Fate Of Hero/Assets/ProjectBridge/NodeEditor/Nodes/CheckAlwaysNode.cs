using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
#endif
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
#if UNITY_EDITOR
            b.alwaysCheckCoun = Mathf.Max(b.alwaysCheckCoun, EditorGUILayout.IntField("size", list.Count));
#endif
            if (!b.collapse)
            {
                while (b.alwaysCheckCoun < list.Count)
                    list.RemoveAt(list.Count - 1);
                while (b.alwaysCheckCoun > list.Count)
                    list.Add(ECondition.IsAlive);

                for (int i = 0; i < list.Count; i++)
                {
#if UNITY_EDITOR
                    list[i] = (ECondition)EditorGUILayout.EnumPopup(list[i]);
                    EditorGUILayout.LabelField("");
#endif
                }
                b.WindowRect.height = b.drawNode.Height + 30 * list.Count;

            }

        }
    }

}