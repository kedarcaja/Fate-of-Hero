using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourEditor
{
	[CreateAssetMenu(menuName = "BehaviourEditor/Nodes/RandomMove")]
	public class RandomMoveNode : ExecutableNode
	{
		public override void DrawCurve(BaseNode node)
		{
		}

		public override void DrawWindow(BaseNode b)
		{
#if UNITY_EDITOR

			BehaviourEditor.GetEGLLable("area: ", GUIStyle.none);
			b.randomMoveArea = EditorGUILayout.TextField(b.randomMoveArea);
#endif
		}

		public override void Execute(BaseNode b)
		{


			Transform t = null;
			foreach (GameObject g in GameObject.FindObjectsOfType(typeof(GameObject)))
			{

				if (g.name == b.randomMoveArea)
				{
					t = g.transform;
				}
			}
			if (t != null && t.GetComponent<RandomMoveArea>() != null && !b.randomSet)
			{
				b.BehaviourGraph.character.RandomMove(t.GetComponent<RandomMoveArea>());
				b.randomSet = true;
			}
			if (b.randomSet && b.BehaviourGraph.character.AgentReachedTarget())
			{
				b.nodeCompleted = true;
				b.randomSet = false;

			}

		}
	}

}