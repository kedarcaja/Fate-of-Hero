using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using Invector;

[CanEditMultipleObjects]
[CustomEditor(typeof(testicek1), true)]
public class TesticekGUI : Editor{

    GUISkin skin;

    public override void OnInspectorGUI()
    {
        if (!skin) skin = Resources.Load("skin") as GUISkin;
        GUI.skin = skin;

        GUILayout.BeginVertical("INPUT MANAGER", "window");


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();

        base.OnInspectorGUI();

        GUILayout.Space(20);


        GUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        

        GUILayout.Space(10);
    }
}
