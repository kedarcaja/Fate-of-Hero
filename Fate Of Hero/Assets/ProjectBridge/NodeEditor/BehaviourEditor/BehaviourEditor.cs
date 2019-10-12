using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditor;
using UnityEditor;
using System.Linq;

namespace BehaviourEditor
{
    public class BehaviourEditor : NodeEditor.NodeEditor
    {
        public static NodeEditor.EditorSettings settings;
        public static DrawNodeHolder DrawNodes;

        private void OnEnable()
        {
            titleContent.text = "Behaviour Editor";
            settings = AssetDatabase.LoadAssetAtPath("Assets/ProjectBridge/NodeEditor/BehaviourEditor/Resources/Editor/Settings.asset", typeof(NodeEditor.EditorSettings)) as NodeEditor.EditorSettings;
            DrawNodes = AssetDatabase.LoadAssetAtPath("Assets/ProjectBridge/NodeEditor/Resources/DrawNodesHolder.asset", typeof(DrawNodeHolder)) as DrawNodeHolder;


        }
        protected override void OnGUI()
        {
            EditorGUI.DrawRect(all, settings.backgroundColor);
            DrawGrid(_zoom * 10 + settings.gridSpacing, settings.gridOpacity, settings.gridColor);
            base.OnGUI();
        }

        [MenuItem("Behaviour Editor/Editor")]
        static void ShowEditor()
        {
            BehaviourEditor editor = GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(800, 600);
        }


        protected override void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            AddNewItemToMenu(menu, "Add Comment", UserActions.commentNode);
            AddNewItemToMenu(menu, "Animator/Add AnimatorHandler", UserActions.AnimatorHandleNode);
            AddNewItemToMenu(menu, "Animator/Add Animator Swap", UserActions.animatorSwapNode);
            AddNewItemToMenu(menu, "Add Condition", UserActions.conditionNode);
            AddNewItemToMenu(menu, "Add Check Always", UserActions.checkAlwaysNode);
            AddNewItemToMenu(menu, "Move/Add Set Destination", UserActions.SetDestinationNode);
            AddNewItemToMenu(menu, "Move/Add RandomMove", UserActions.randomMoveNode);
            AddNewItemToMenu(menu, "Add Delay", UserActions.delayNode);
            AddNewItemToMenu(menu, "Add Graph Swap", UserActions.graphSwapNode);
            AddNewItemToMenu(menu, "Add Portal", UserActions.portalNode);
            AddNewItemToMenu(menu, "Move/Add Stop", UserActions.stopNode);


            menu.ShowAsContext();
            e.Use();
        }

        protected override void ContextCallback(object o)
        {
            UserActions a = (UserActions)o;

            switch (a)
            {
                case UserActions.deleteNode:

                    if (currentGraph.IsEnterState(currentGraph.EnterNode, selectedNode))
                    {
                        if (currentGraph.nodes.Exists(f => f != selectedNode && !(f.drawNode is EnterNode)))
                            currentGraph.SetAsEnterState(currentGraph.EnterNode, currentGraph.nodes.FirstOrDefault(f => f != selectedNode && !(f.drawNode is EnterNode)), GUIStylizer.Colors.GREEN);
                    }
                    currentGraph.removeNodesIDs.Add(selectedNode.ID);
                    break;
                case UserActions.makeTransition:
                    isMakingTransition = true;
                    break;
                case UserActions.conditionNode:
                    currentGraph.AddNode(DrawNodes.ConditionNode, mousePosition.x, mousePosition.y, "Condition");
                    break;
                case UserActions.checkAlwaysNode:
                    currentGraph.AddNode(DrawNodes.CheckAlwaysNode, mousePosition.x, mousePosition.y, "Check Always");
                    break;

                case UserActions.SetDestinationNode:
                    currentGraph.AddNode(DrawNodes.SetDestinationNode, mousePosition.x, mousePosition.y, "Set Destination");
                    break;

                case UserActions.commentNode:
                    currentGraph.AddNode(DrawNodes.CommentNode, mousePosition.x, mousePosition.y, "Comment");
                    break;
                case UserActions.AnimatorHandleNode:
                    currentGraph.AddNode(DrawNodes.AnimatorHandleNode, mousePosition.x, mousePosition.y, "Animator Handler");
                    break;

                case UserActions.delayNode:
                    currentGraph.AddNode(DrawNodes.DelayNode, mousePosition.x, mousePosition.y, "Delay");
                    break;

                case UserActions.portalNode:
                    currentGraph.AddNode(DrawNodes.PortalNode, mousePosition.x, mousePosition.y, "Portal");
                    break;
                case UserActions.stopNode:
                    currentGraph.AddNode(DrawNodes.StopNode, mousePosition.x, mousePosition.y, "Stop Move");
                    break;

                case UserActions.randomMoveNode:
                    currentGraph.AddNode(DrawNodes.RandomMoveNode, mousePosition.x, mousePosition.y, "Random Move");
                    break;
                case UserActions.animatorSwapNode:
                    currentGraph.AddNode(DrawNodes.AnimatorSwapNode, mousePosition.x, mousePosition.y, "Animator Swap");
                    break;
                case UserActions.graphSwapNode:
                    currentGraph.AddNode(DrawNodes.GraphSwapNode, mousePosition.x, mousePosition.y, "Graph Swap");
                    break;
                case UserActions.setAsStartNode:
                    currentGraph.SetAsEnterState(currentGraph.EnterNode, selectedNode, GUIStylizer.Colors.GREEN);
                    break;
            }


            base.ContextCallback(o);
        }


        public override void DrawWindows()
        {
            base.DrawWindows();
            Rect zone = new Rect(0, 0, 200, 100);
            EditorGUI.DrawRect(zone, settings.otherGUIColor);
            GUILayout.BeginArea(new Rect(zone.x + 2, zone.y + 2, zone.width, zone.height));
            GetEGLLable("Character: ", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleCenter, 20));
            currentGraph = (BehaviourGraph)EditorGUILayout.ObjectField(currentGraph, typeof(BehaviourGraph), false, GUILayout.Width(200)); // field to choose graph

            if (GUI.Button(new Rect(43, 50, 100, 20), "New graph"))
            {
                ScriptableObjectUtility.CreateAsset<BehaviourGraph>("NewBehaviourGraph");
            }
            GUILayout.EndArea();


            if (currentGraph == null)
            {

                GUILayout.BeginArea(new Rect(150, 300, 1920, 200));

                GetEGLLable("No Character Assign!", GUIStylizer.GetStyle(GUIStylizer.Colors.RED, TextAnchor.MiddleLeft, 200, 0, 0, 130, 0f));
                GUILayout.EndArea();
            }
        }
    }
}
