using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourEditor
{
	[CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Animator/Handling")]
	public class AnimatorHandleNode : ExecutableNode
	{
		public override void DrawCurve(BaseNode node)
		{
		}

		public override void DrawWindow(BaseNode b)
		{
#if UNITY_EDITOR

            BehaviourEditor.GetEGLLable("Layer: ", GUIStyle.none);
            b.animationLayer = EditorGUILayout.IntField(b.animationLayer);

            b.AnimatorActivatorType = (EAnimatorActivator)EditorGUILayout.EnumPopup(b.AnimatorActivatorType);

			BehaviourEditor.GetEGLLable("parameter: ", GUIStyle.none);
			b.parameter = EditorGUILayout.TextField(b.parameter);

			BehaviourEditor.GetEGLLable("Value: ", GUIStyle.none);
			switch (b.AnimatorActivatorType)
			{
				case EAnimatorActivator.Trigger:
					BehaviourEditor.GetEGLLable("Trigger", GUIStyle.none);
					break;
				case EAnimatorActivator.Float:
					b.AnimatorActivatorFloatValue = EditorGUILayout.FloatField(b.AnimatorActivatorFloatValue);
					break;
				case EAnimatorActivator.Bool:
					b.AnimatorActivatorBoolValue = EditorGUILayout.Toggle(b.AnimatorActivatorBoolValue);
					break;
				case EAnimatorActivator.Int:
					b.AnimatorActivatorIntValue = EditorGUILayout.IntField(b.AnimatorActivatorIntValue);
					break;

			}
#endif
        }

		public override void Execute(BaseNode b)
		{
			switch (b.AnimatorActivatorType)
			{
				case EAnimatorActivator.Trigger:
					b.BehaviourGraph.character.Animator.SetTrigger(b.parameter);
					break;
				case EAnimatorActivator.Float:
					b.BehaviourGraph.character.Animator.SetFloat(b.parameter,b.AnimatorActivatorFloatValue);

					break;
				case EAnimatorActivator.Bool:
					b.BehaviourGraph.character.Animator.SetBool(b.parameter,b.AnimatorActivatorBoolValue);

					break;
				case EAnimatorActivator.Int:
					b.BehaviourGraph.character.Animator.SetInteger(b.parameter,b.AnimatorActivatorIntValue);
					break;
			}
            b.BehaviourGraph.character.Animator.SetLayerWeight(b.animationLayer, 1);
            b.nodeCompleted = true;
		}
	}
}