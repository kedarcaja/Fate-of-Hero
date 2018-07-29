using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(QuestManager))]
public class QuestEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("New Quest",GUILayout.Width(80),GUILayout.Height(30)))
		{
			 (target as QuestManager).gameObject.AddComponent<Quest>();
			
		}

	}







}
#endif