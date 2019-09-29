using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

[CustomEditor(typeof(Character), true)]
public class CharacterEditor : Editor
{
    private Character character;
    private static GUIStyle ToggleButtonStyleNormal = null;
    private static GUIStyle ToggleButtonStyleToggled = null;
    bool mainSmooth, statsSmooth = false;
    bool mainToggle = true, statsToggle = false;
    private void OnEnable()
    {
        character = target as Character;
    }

    public override void OnInspectorGUI()
    {
        //create toggle button
        if (ToggleButtonStyleNormal == null)
        {
            ToggleButtonStyleNormal = "Button";
            ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);
            ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
        }
        GUIStyle _style = new GUIStyle();





        GUILayout.BeginVertical(EditorStyles.helpBox);
        if (GUILayout.Button("MainContent", mainToggle ? ToggleButtonStyleToggled : ToggleButtonStyleNormal, GUILayout.Width(400)))
        {
            mainToggle = !mainToggle;
            mainSmooth = !mainSmooth;

        }
        if (mainToggle)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.BeginHorizontal();

            //image
            character.Portait = (Sprite)EditorGUILayout.ObjectField(character.Portait, typeof(Sprite), false, GUILayout.Width(150), GUILayout.Height(150));
            GUILayout.BeginVertical(EditorStyles.helpBox);
            //character class 
            GUILayout.BeginHorizontal();
            GUILayout.Label("Class: ");
            character.CharacterClass = (ECharacterClass)EditorGUILayout.EnumPopup(character.CharacterClass, GUILayout.Width(180));
            GUILayout.EndHorizontal();
            //work
            GUILayout.BeginHorizontal();
            GUILayout.Label("Work: ");
            character.Work = (EWork)EditorGUILayout.EnumPopup(character.Work, GUILayout.Width(180));
            GUILayout.EndHorizontal();
            //mood
            GUILayout.BeginHorizontal();
            GUILayout.Label("Mood: ");
            character.Mood = (EMood)EditorGUILayout.EnumPopup(character.Mood, GUILayout.Width(180));
            GUILayout.EndHorizontal();
            //level
            EditorGUIUtility.labelWidth = 62;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Level"), GUILayout.Width(150));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            //name
            GUIStyle nameStyle = new GUIStyle(GUI.skin.label) { fontSize = 15, fixedHeight = 30 };
            nameStyle.normal.textColor = character.Color;
            EditorGUILayout.LabelField(character.name, nameStyle);
            //color
            EditorGUIUtility.labelWidth = 40;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Color"), GUILayout.Width(180));
            character.Color = new Color(character.Color.r, character.Color.g, character.Color.b, 1);
            GUILayout.EndVertical();

        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical(EditorStyles.helpBox);

        if (GUILayout.Button("Stats", statsToggle ? ToggleButtonStyleToggled : ToggleButtonStyleNormal, GUILayout.Width(400)))
        {
            statsToggle = !statsToggle;
            statsSmooth = !statsSmooth;

        }
        if (statsToggle)
            DrawDefaultInspector();
        GUILayout.EndVertical();


        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
        EditorUtility.SetDirty(target);

    }
}
#endif
