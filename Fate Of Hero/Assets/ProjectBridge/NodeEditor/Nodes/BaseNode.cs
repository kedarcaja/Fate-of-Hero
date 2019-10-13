using BehaviourEditor;
using DialogEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Data;
#if UNITY_EDITOR

using UnityEditorInternal;
#endif
using UnityEngine;
public enum EAnimatorActivator { Trigger, Float, Bool, Int }
namespace NodeEditor
{
    [Serializable]
    public class BaseNode
    {

        #region Variables
        public BehaviourGraph BehaviourGraph;
        public DialogGraph DialogGraph;
        public string ID;
        public Rect WindowRect;
        public string WindowTitle;
        public Color32 nodeColor = Color.grey;
        public bool collapse = false;
        public DrawNode drawNode;
        public bool previousCollapse;
        [SerializeField]
        public List<Transition> transitions = new List<Transition>();
        [SerializeField]
        public List<Transition> depencies = new List<Transition>();
        public List<string> transitionsIdsToRemove = new List<string>();
        public bool nodeCompleted = false;
        public bool deletable = true;
        public bool focused = false;
        #endregion
        #region Executable nodes
        public bool executed = false;
        #endregion

        #region Comment node Variables
        public string comment = "";
        #endregion

        #region Condition node Variables
        public ECondition condition;
        #endregion
        #region Animator nodes Variables

        public string parameter;
        public EAnimatorActivator AnimatorActivatorType;
        public bool AnimatorActivatorBoolValue;
        public int AnimatorActivatorIntValue;
        public float AnimatorActivatorFloatValue;
        public int animationLayer = 0;

        #region Animator swap nodes Variables
        public RuntimeAnimatorController animatorController;
        #endregion
        #endregion
        #region Timing nodes Variables
        public _Timer timer;
        public float delay;
        #endregion
        #region Random move nodes Variables
        public string randomMoveArea;
        public bool randomSet = false;
        #endregion
        #region Portal nodes Variables
        public string portalTargetNodeID;
        #endregion
        #region SetDestination node Variables
        public Transform destinationTarget;
        public string destinationTargetName;
        #endregion
        #region Check always Node
        public List<ECondition> alwaysCheckConditions = new List<ECondition>();
        public int alwaysCheckCoun = 1;
        #endregion
        #region Graph swap node
        public BehaviourGraph swapGraph;
        #endregion
        #region Dialog audio nodes
        public AudioClip dialogAudioClip;
        #endregion
        #region Dialog part nodes
        public string dialogPartSubtitles = "";
        public Character dialogPartspeaker;
        public float dialogPartStartDuration = 0;
        #endregion
        #region Dialog event nodes
        public int eventListSize = 1;
        public List<EDialogEvents> dialogEvents = new List<EDialogEvents>();
        public List<ItemReward> addItems = new List<ItemReward>();
        #endregion
        #region Dialog decision nodes
        public int decisionSelectedOption = 10;
        public bool decided = false;
        #endregion
        public string GetTransitionId(char end)
        {
            return "T" + DateTime.Now.Second.ToString() + transitions.Count.ToString() + end;
        }
        public BaseNode(DrawNode draw, float x, float y, string title, string id)
        {
            ID = id;
            WindowRect = new Rect(x, y, draw.Width, draw.Height);
            WindowTitle = title;
            drawNode = draw;
            nodeColor = draw.NodeColor;
        }

        public void AddTransitionsToRemove(string id)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].ID == id)
                {
                    transitionsIdsToRemove.Add(id);
                }
            }
        }
        public void RemoveTransitions()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitionsIdsToRemove.Contains(transitions[i].ID))
                {
                    transitions.Remove(transitions[i]);
                }
            }
        }
        public void DrawWindow()
        {
#if UNITY_EDITOR
         
            Rect zone = new Rect(0, 17, WindowRect.width, WindowRect.height - 17);
            EditorGUI.DrawRect(new Rect(0, 17, WindowRect.width, WindowRect.height - 17), nodeColor);
            if (focused)
            {
                EditorGUI.DrawRect(new Rect(0, 0, WindowRect.width,1), GUIStylizer.Colors.LIGHTSKYBLUE);
                EditorGUI.DrawRect(new Rect(0, 0, 1,WindowRect.height), GUIStylizer.Colors.LIGHTSKYBLUE);
                EditorGUI.DrawRect(new Rect(WindowRect.width-1, 0, 1,WindowRect.height), GUIStylizer.Colors.LIGHTSKYBLUE);
                EditorGUI.DrawRect(new Rect(0, WindowRect.height-1, WindowRect.width,1), GUIStylizer.Colors.LIGHTSKYBLUE);
            }
            if (drawNode.enableCollapse)
            {
                collapse = EditorGUILayout.Toggle(collapse);
                if (collapse)
                {
                    WindowRect.height = drawNode.collapseHeight;
                    WindowRect.width = drawNode.Width;
                }
                else
                {
                    WindowRect.height = drawNode.Height;
                    WindowRect.width = drawNode.scaledWidth;

                }
            }

            drawNode?.DrawWindow(this);
#endif
        }
        public void DrawCurve()
        {
            foreach (Transition t in transitions)
            {
                if (t == null || t.endNode == null || t.startNode == null || WindowRect.size == Vector2.zero) continue;
#if UNITY_EDITOR

                NodeEditor.DrawNodeCurve(t, t.startNode.WindowRect, t.endNode.WindowRect, t.startPlacement, t.endPlacement, t.Color, t.disabled);
#endif
                t.DrawConnection(t.startNode, t.endNode, t.startPlacement, t.endPlacement, t.Color, t.disabled);
            }
            drawNode?.DrawCurve(this);
        }
        public void Execute()
        {
            if (!(drawNode is ExecutableNode)) return;

            (drawNode as ExecutableNode).Execute(this);
            executed = true;

        }
    }
}

