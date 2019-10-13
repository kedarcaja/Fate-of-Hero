using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Animator/AnimatorSwap")]
    public class AnimatorSwapNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR
            b.animatorController = EditorGUILayout.ObjectField(b.animatorController, typeof(RuntimeAnimatorController), false) as RuntimeAnimatorController;
#endif
        }

        public override void Execute(BaseNode b)
        {
            b.BehaviourGraph.character.Animator.runtimeAnimatorController = b.animatorController;
            b.nodeCompleted = b.BehaviourGraph.character.Animator.runtimeAnimatorController == b.animatorController;
        }
    }
}