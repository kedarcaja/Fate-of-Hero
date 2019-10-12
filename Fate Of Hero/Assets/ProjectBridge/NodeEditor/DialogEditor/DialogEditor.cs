using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DialogEditor
{

    public class DialogEditor : NodeEditor.NodeEditor
    {

        public static NodeEditor.EditorSettings settings;
        public static DrawNodeHolder DrawNodes;
        private bool showDialogSettings = true;
        private void OnEnable()
        {
            titleContent.text = "Dialog Editor";
            settings = AssetDatabase.LoadAssetAtPath("Assets/ProjectBridge/NodeEditor/DialogEditor/Resources/Editor/Settings.asset", typeof(NodeEditor.EditorSettings)) as NodeEditor.EditorSettings;
            DrawNodes = AssetDatabase.LoadAssetAtPath("Assets/ProjectBridge/NodeEditor/Resources/DrawNodesHolder.asset", typeof(DrawNodeHolder)) as DrawNodeHolder;
        }

        protected override void OnGUI()
        {
            //EditorGUI.DrawRect(all, settings.backgroundColor);
            DrawGrid(_zoom * 10 + settings.gridSpacing, settings.gridOpacity, settings.gridColor);
            base.OnGUI();
        }
        [MenuItem("Dialog Editor/Editor")]
        static void ShowEditor()
        {
            DialogEditor editor = GetWindow<DialogEditor>();
            editor.minSize = new Vector2(800, 600);
        }

        protected override void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            if (selectedNode.drawNode.Deletable && selectedNode.deletable)
                AddNewItemToMenu(menu, "Delete", UserActions.deleteNode);
            if (TransitionEnable())
                AddNewItemToMenu(menu, "Make Transition", UserActions.makeTransition);

            if ((selectedNode != currentGraph.EnterNode && currentGraph.EnterNode.transitions[0]?.endNode != selectedNode)&&!currentGraph.IsEnterState((currentGraph as DialogGraph).firstSubtitles,selectedNode))
                AddNewItemToMenu(menu, "Set As StartState", UserActions.setAsStartNode);

            if (selectedNode.drawNode is DialogDecisionNode && selectedNode.drawNode.transitionsLimit > selectedNode.transitions.Count)
            {

                AddNewItemToMenu(menu, "Add Option Branch", UserActions.dialogDecisionBranch);
            }
            if (selectedNode.drawNode.duplicatable)
                AddNewItemToMenu(menu, "Duplicate", UserActions.duplicateSelection);

            menu.ShowAsContext();
            e.Use();


        }
        protected override void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            AddNewItemToMenu(menu, "Add Dialog Part", UserActions.dialogPartNode);
            AddNewItemToMenu(menu, "Add Decision", UserActions.dialogDecisionNode);
            AddNewItemToMenu(menu, "Add Event", UserActions.dialogEventNode);


            menu.ShowAsContext();
            e.Use();
        }

        protected override void ContextCallback(object o)
        {
            UserActions a = (UserActions)o;

            switch (a)
            {
                case UserActions.deleteNode:
                    if (!selectedNode.deletable) return;
                    if ((currentGraph as DialogGraph).IsFirstSubtitles(selectedNode))
                    {
                        if (currentGraph.nodes.Exists(f => f != selectedNode && !(f.drawNode is EnterNode) && !(f.drawNode is DialogNode)))
                            (currentGraph as DialogGraph).SetAsEnterState((currentGraph as DialogGraph).firstSubtitles, currentGraph.nodes.Find(f => f != selectedNode && !(f.drawNode is EnterNode) && !(f.drawNode is DialogNode)), GUIStylizer.Colors.ORANGE);
                    }
                    if(selectedNode.drawNode is DialogNode)
                    {
                        for (int i = 0; i < selectedNode.transitions.Count; i++)
                        {
                            selectedNode.AddTransitionsToRemove(selectedNode.transitions[i].ID);
                        }
                    }
                    if (selectedNode.drawNode is DialogDecisionNode)
                    {
                        (currentGraph as DialogGraph).RemoveDecisionBranch(selectedNode);
                    }
                    currentGraph.removeNodesIDs.Add(selectedNode.ID);
                  
                    selectedNode = null;

                    break;
                case UserActions.dialogPartNode:
                    BaseNode n = currentGraph.AddNode(DrawNodes.DialogPartNode, mousePosition.x, mousePosition.y, "Dialog Subtitles");
                    break;
                case UserActions.setAsStartNode:

                    (currentGraph as DialogGraph).SetAsEnterState((currentGraph as DialogGraph).firstSubtitles, selectedNode, GUIStylizer.Colors.ORANGE);
                    break;
                case UserActions.makeTransition:
                    isMakingTransition = true;
                    break;
                case UserActions.dialogDecisionNode:
                    currentGraph.AddNode(DrawNodes.DialogDecisionNode, mousePosition.x, mousePosition.y, "Decision");
                    break;
                case UserActions.dialogEventNode:
                    currentGraph.AddNode(DrawNodes.DialogEventNode, mousePosition.x, mousePosition.y, "Event");
                    break;

                case UserActions.dialogDecisionBranch:
                    (currentGraph as DialogGraph).AddDecisionBranch(selectedNode, mousePosition.x, mousePosition.y);
                    break;
                case UserActions.duplicateSelection:
                    if (currentGraph.selectedNodes.Count > 0)
                    {
                        currentGraph.DuplicateSelection(mousePosition.x, mousePosition.y);
                    }
                    else
                    {
                        currentGraph.AddNode(selectedNode.drawNode, mousePosition.x, mousePosition.y, selectedNode.WindowTitle);
                    }
                    selectedNode = null;

                    break;
            }

            base.ContextCallback(o);
        }


        public override void DrawWindows()
        {
            base.DrawWindows();
            #region Graph field
            Rect zone = new Rect(0, 0, 200, 100);
            EditorGUI.DrawRect(zone, settings.otherGUIColor);
            GUILayout.BeginArea(new Rect(zone.x + 2, zone.y + 2, zone.width, zone.height));
            GetEGLLable("Dialog: ", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));
            currentGraph = (NodeGraph)EditorGUILayout.ObjectField(currentGraph, typeof(DialogGraph), false, GUILayout.Width(200)); // field to choose graph
            if (GUI.Button(new Rect(43, 50, 100, 20), "New graph"))
            {
                currentGraph = ScriptableObjectUtility.CreateAsset<DialogGraph>("NewDialogGraph");
            }
            GUILayout.EndArea();

            if (currentGraph == null)
            {

                GUILayout.BeginArea(new Rect(150, 300, 1920, 200));

                GetEGLLable("No Dialog Assign!", GUIStylizer.GetStyle(GUIStylizer.Colors.RED, TextAnchor.MiddleLeft, 200, 0, 0, 130, 0f));
                GUILayout.EndArea();
            }
            #endregion


            if (currentGraph != null)
            {

                GUILayout.BeginArea(new Rect(205, 20, 150, 50));
                GUILayout.BeginHorizontal();
                showDialogSettings = GUILayout.Toggle(showDialogSettings, "");
                GetEGLLable("Show settings", GUIStylizer.GetStyle(GUIStylizer.Colors.REDPING, TextAnchor.MiddleLeft, 15));

                GUILayout.EndHorizontal();

                GUILayout.EndArea();

                if (showDialogSettings)
                {
                    DialogGraph d = currentGraph as DialogGraph;
                    Rect sett = new Rect(355, 10, 1500, 50);
                    GUIStyle s = GUIStylizer.GetStyle(settings.otherGUIColor, TextAnchor.UpperCenter, 20, 0, 0, 10, 0f);
                    EditorGUI.DrawRect(sett, settings.otherGUIColor);
                    GUI.contentColor = Color.white;
                    float spacing = -70;
                    if (d.unlimitedRepeating)
                    {
                        spacing = -150;
                    }
                    if (d.repeatable)
                    {
                        spacing = 0;
                    }

                    GUILayout.BeginArea(sett, s);
                    GUILayout.BeginHorizontal();



                    d.IsEnable = GUILayout.Toggle(d.IsEnable, "");
                    GUILayout.Space(spacing);
                    GetEGLLable("Enable", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));

                    d.unlimitedRepeating = GUILayout.Toggle(d.unlimitedRepeating, "");
                    GUILayout.Space(spacing);
                    GetEGLLable("Unlimited Repeating", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));


                    if (!d.unlimitedRepeating)
                    {

                        d.repeatable = GUILayout.Toggle(d.repeatable, "");
                        GUILayout.Space(spacing);
                        GetEGLLable("Repeatable", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));


                        if (d.repeatable)
                        {
                            GetEGLLable("Repeat Limit", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));
                            GUILayout.Space(spacing);
                            d.repeatLimit = EditorGUILayout.IntField(d.repeatLimit, "");

                        }
                    }
                    d.skipable = GUILayout.Toggle(d.skipable, "");
                    GUILayout.Space(spacing);
                    GetEGLLable("Skipable", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));


                    d.destroyOnEnd = GUILayout.Toggle(d.destroyOnEnd, "");
                    GUILayout.Space(spacing);
                    GetEGLLable("Destroy On End", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleLeft, 20));

                    GUILayout.EndHorizontal();
                    GUILayout.EndArea();

                }
            }
        }
    }
}
