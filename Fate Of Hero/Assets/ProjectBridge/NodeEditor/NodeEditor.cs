using DialogEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

namespace NodeEditor
{
#if UNITY_EDITOR
    public class NodeEditor : EditorWindow
    {
        #region Variables
        protected Vector3 mousePosition;
        protected bool clickedOnWindow;
        public BaseNode selectedNode;
        public NodeGraph currentGraph;
        public bool isMakingTransition = false;
        protected Rect all = new Rect(0, 0, 10000, 10000); // window 
        protected bool showSettings = false;
        protected Transition selectedTransition;
        #region Window Handle Variables
        protected const float kZoomMin = 0.3f;
        protected const float kZoomMax = 1f;
        protected readonly Rect _zoomArea = new Rect(0, 0, 10000, 10000);
        protected float _zoom = 1.0f;
        protected Vector2 _zoomCoordsOrigin = Vector2.zero;
        protected Vector2 scrollPos;
        protected Vector2 scrollStartPos;
        protected Vector2 offset;
        protected Vector2 drag;
        #endregion

        #region SelectionZone
        protected bool creatingSelectionZone = false;
        protected Vector2 selectionBoxStartPos;
        protected Vector2 selectionBoxCurrentPos;
        #endregion

        #endregion
        public enum UserActions
        {
            deleteNode, commentNode, AnimatorHandleNode, makeTransition, conditionNode, SetDestinationNode, delayNode, portalNode, randomMoveNode, animatorSwapNode, checkAlwaysNode, stopNode, graphSwapNode, setAsStartNode,
            dialogPartNode, dialogDecisionNode, dialogEventNode, dialogDecisionBranch, duplicateSelection
        }


        #region Unity Methods

     
        protected virtual void OnGUI()
        {

            Event e = Event.current;

            mousePosition =new Vector2(e.mousePosition.x,e.mousePosition.y+20);



            UserInput(e);
            DrawWindows();

            if (creatingSelectionZone)
            {
                EditorGUI.DrawRect(new Rect(selectionBoxStartPos.x, selectionBoxStartPos.y, selectionBoxCurrentPos.x - selectionBoxStartPos.x, selectionBoxCurrentPos.y - selectionBoxStartPos.y), new Color(0.3f, 0.3f, 0.3f, 0.4f));
            }

            if (e.isMouse) e.Use();

            EditorGUI.DrawRect(new Rect(mousePosition, Vector2.one), Color.red);
            if (GUI.changed)
            {
                Repaint();
            }
            if (isMakingTransition)
            {
                DrawNodeCurve(null, selectedNode.WindowRect, new Rect(mousePosition.x, mousePosition.y, 20, 20), EWindowCurvePlacement.Center, EWindowCurvePlacement.Center, Color.black, false);
                Repaint();
            }

        }
        #endregion
        #region Window Handle Methods
        /// <summary>
        /// Enables moving in window by draging mouse
        /// </summary>
        /// <param name="e"></param>
        void HandlePanning(Event e)
        {
            Vector2 diff = e.mousePosition - scrollStartPos;
            diff *= .6f;
            scrollStartPos = e.mousePosition;
            scrollPos += diff;

            for (int i = 0; i < currentGraph.nodes.Count; i++)
            {
                BaseNode b = currentGraph.nodes[i];
                b.WindowRect.x += diff.x;
                b.WindowRect.y += diff.y;
            }
            e.Use();
        }
        /// <summary>
        /// Resets position of window
        /// </summary>
        void ResetScroll()
        {
            for (int i = 0; i < currentGraph.nodes.Count; i++)
            {
                BaseNode b = currentGraph.nodes[i];
                b.WindowRect.x -= scrollPos.x;
                b.WindowRect.y -= scrollPos.y;
            }

            scrollPos = Vector2.zero;
        }
        protected Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - _zoomArea.TopLeft()) / _zoom + _zoomCoordsOrigin;
        }
        /// <summary>
        /// Handles window zoom
        /// </summary>
        /// <param name="e"></param>
        protected void HandleZoom(Event e)
        {

            _zoomCoordsOrigin = e.mousePosition;
            Vector2 screenCoordsMousePos = e.mousePosition;
            Vector2 delta = e.delta;
            Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
            float zoomDelta = -delta.y / 150.0f;
            float oldZoom = _zoom;
            _zoom += zoomDelta;
            _zoom = Mathf.Clamp(_zoom, kZoomMin, kZoomMax);
            _zoomCoordsOrigin += (zoomCoordsMousePos - _zoomCoordsOrigin) - (oldZoom / _zoom) * (zoomCoordsMousePos - _zoomCoordsOrigin);

            Repaint();


            e.Use();
        }

        /// <summary>
        /// Draws grid in window
        /// </summary>
        /// <param name="gridSpacing">spacing between grid lines</param>
        /// <param name="gridOpacity">opacity of grid lines</param>
        /// <param name="gridColor">color of grid lines</param>
        protected void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
        #endregion
        #region Input Handle Methods

        public void HandleGraphSelection(Event e)
        {
            clickedOnWindow = false;
            if (selectedTransition != null && !new Rect(10, 150, 200, 300).Contains(e.mousePosition))
            {
                selectedTransition.clicked = false;
                selectedTransition = null;
            }
            for (int i = 0; i < currentGraph.nodes.Count; i++)
            {
                Rect r = currentGraph.nodes[i].WindowRect;
                if (new Rect(r.position*_zoom,r.size*_zoom).Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = currentGraph.nodes[i];

                    break;
                }
            }
        }

        protected void UserInput(Event e)
        {


            if (currentGraph == null) return;

            BaseNode start = selectedNode;
            if (e.modifiers == EventModifiers.Control && e.keyCode == KeyCode.D && e.type == EventType.KeyUp)
            {
                if (currentGraph.selectedNodes.Count > 0)
                {
                    currentGraph.DuplicateSelection(mousePosition.x, mousePosition.y);
                }
                else
                {
                    if (selectedNode != null)
                        currentGraph.AddNode(selectedNode.drawNode, mousePosition.x, mousePosition.y, selectedNode.WindowTitle);
                }
                selectedNode = null;
                Repaint();
            }
            if (e.button == 1)
            {
                if (e.type == EventType.MouseDown)
                {
                    HandleGraphSelection(e);

                    RightClick(e);
                }
            }
            if (e.button == 0)
            {
                if (e.type == EventType.MouseDrag)
                {
                    if (clickedOnWindow && currentGraph.selectedNodes.Count > 0)
                    {
                        for (int i = 0; i < currentGraph.selectedNodes.Count; i++)
                        {
                            currentGraph.selectedNodes[i].WindowRect.position += e.delta;
                        }
                    }
                    selectionBoxCurrentPos = mousePosition;
                    if (!creatingSelectionZone && !clickedOnWindow && e.modifiers == EventModifiers.Shift)
                    {
                        creatingSelectionZone = true;
                        selectionBoxStartPos = selectionBoxCurrentPos;
                    }
                }

                if (e.type == EventType.MouseUp)
                {


                    if (creatingSelectionZone)
                    {
                        selectionBoxCurrentPos = mousePosition;
                        currentGraph.selectedNodes.Clear();
                        Rect r = new Rect(selectionBoxStartPos.x, selectionBoxStartPos.y, selectionBoxCurrentPos.x - selectionBoxStartPos.x, selectionBoxCurrentPos.y - selectionBoxStartPos.y);

                        foreach (BaseNode b in currentGraph.nodes)
                        {
                            if (r.Contains(b.WindowRect.position))
                            {
                                if (currentGraph.selectedNodes.Exists(s => s.ID == b.ID)) continue;

                                currentGraph.selectedNodes.Add(b);
                                b.focused = true;
                            }
                        }
                        creatingSelectionZone = false;
                    }
                }


                if (e.type == EventType.MouseDown)
                {
                    HandleGraphSelection(e);

                    if (isMakingTransition)
                    {
                        BaseNode end = selectedNode;
                        MakeTransition(start, end);
                    }
                    if (!clickedOnWindow)
                    {
                        foreach (BaseNode b in currentGraph.selectedNodes)
                        {
                            b.focused = false;
                        }
                        currentGraph.selectedNodes.Clear();
                    }
                }

            }

            if (e.button == 2 || e.button == 0 && e.modifiers == EventModifiers.Alt)
            {
                if (e.type == EventType.MouseDown && !clickedOnWindow)
                {
                    scrollStartPos = e.mousePosition;
                }
                else if (e.type == EventType.MouseDrag && !clickedOnWindow)
                {
                    HandlePanning(e);
                }
            }
            if (e.Equals(Event.KeyboardEvent("delete")) && (selectedNode != null || currentGraph.selectedNodes.Count > 0))
            {
                GUI.FocusControl(null); // removes focus from graph gui
                if (selectedNode != null && selectedNode.deletable && selectedNode.drawNode.Deletable)
                    currentGraph.removeNodesIDs.Add(selectedNode.ID);

                currentGraph.nodes.ForEach(f => { if (currentGraph.selectedNodes.Exists(n => n.ID == f.ID)) { if (f.deletable && f.drawNode.Deletable) currentGraph.removeNodesIDs.Add(f.ID); } });
                currentGraph.UnFocusSelectedNodes();
                currentGraph.selectedNodes.Clear();


                Repaint();
            }
            if (e.type == EventType.ScrollWheel)
            {
                HandleZoom(e);
            }
        }

        public void RightClick(Event e)
        {
            if (currentGraph == null) return;

            if (!clickedOnWindow)
            {
                selectedNode = null;
                currentGraph.UnFocusSelectedNodes();
                currentGraph.selectedNodes.Clear();
                isMakingTransition = false;


                AddNewNode(e);

            }
            else
            {
                ModifyNode(e);
            }

        }

        #endregion
        #region Node Methods
        public virtual void DrawWindows()
        {
            if (currentGraph != null)
            {
                EditorZoomArea.Begin(_zoom, new Rect(0,0,Screen.width,Screen.width));
                GUILayout.BeginArea(all);
                BeginWindows();

                currentGraph?.RemoveNodeSelectedNodes();

                for (int i = 0; i < currentGraph.nodes.Count; i++)
                {
                    BaseNode n = currentGraph.nodes[i];

                    foreach (Transition t in n.transitions)
                    {
                        if (!currentGraph.nodes.Contains(t.endNode))
                        {
                            n.AddTransitionsToRemove(t.ID);
                            Repaint();
                        }
                    }


                    if (n.WindowRect.size != Vector2.zero) // if node is in zone and zone is not collapsed
                    {
                        EditorGUI.DrawRect(new Rect(n.WindowRect.position, 2 * Vector2.one), Color.red);

                        n.WindowRect = GUI.Window(i, n.WindowRect, DrawNodeWindow, n.WindowTitle + (n.drawNode is EnterNode ? "" : " id: " + n.ID)); // setting up nodes as windows

                    }

                    n.DrawCurve(); // drawing transitions

                }

                EndWindows();
                GUILayout.EndArea();
                EditorZoomArea.End(all);

                foreach (BaseNode b in currentGraph.nodes)
                {
                    foreach (Transition t in b.transitions)
                    {
                        DrawTransitionSettings(t);
                    }
                }
            }

            currentGraph?.RemoveTransitions();
        }
        protected void DrawNodeWindow(int id)
        {
            currentGraph?.nodes[id].DrawWindow();

            if(currentGraph?.nodes[id] == currentGraph?.LifeCycle()?.CurrentNode)
            {
           
                currentGraph.nodes[id].nodeColor = GUIStylizer.Colors.GREEN;

            }
            else
            {
                currentGraph.nodes[id].nodeColor = currentGraph.nodes[id].drawNode.NodeColor;

            }
            GUI.DragWindow();
            Repaint();
        }
        protected virtual void AddNewNode(Event e)
        {

        }
        protected void AddNewItemToMenu(GenericMenu menu, string title, UserActions a)
        {
            menu.AddItem(new GUIContent(title), false, ContextCallback, a);

        }
        protected virtual void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            if (selectedNode.drawNode.Deletable)
                AddNewItemToMenu(menu, "Delete", UserActions.deleteNode);
            if (TransitionEnable())
                AddNewItemToMenu(menu, "Make Transition", UserActions.makeTransition);

            if ((selectedNode != currentGraph.EnterNode && currentGraph.EnterNode.transitions[0]?.endNode != selectedNode))
                AddNewItemToMenu(menu, "Set As StartState", UserActions.setAsStartNode);


            menu.ShowAsContext();
            e.Use();
        }

        protected virtual void ContextCallback(object o)
        {

            EditorUtility.SetDirty(currentGraph);

        }
        #endregion

        #region Transition Methods


        protected bool TransitionEnable()
        {
            return selectedNode.drawNode.EnableTransitions && (selectedNode.transitions.Count < selectedNode.drawNode.transitionsLimit || selectedNode.drawNode.unlimitedTransitions);
        }
        protected static void DrawTransitionClickPoint(Transition t, Vector3 start, Vector3 end)
        {
            if (t == null || t.startNode.drawNode is EnterNode || t.startNode.drawNode is DialogNode) return;
            Handles.color = t.Color;

            if (Handles.Button((start + end) * .5f, Quaternion.identity, 4, 8, Handles.DotHandleCap))
            {
                t.clicked = !t.clicked;
            }

        }
        protected void DrawTransitionSettings(Transition t)
        {
            if (t.clicked)
            {
                if (selectedTransition != null && selectedTransition != t)
                {
                    selectedTransition.clicked = false;

                }
                selectedTransition = t;

                EditorGUI.DrawRect(new Rect(10, 150, 200, 300), DialogEditor.DialogEditor.settings.otherGUIColor);

                GUILayout.BeginArea(new Rect(10, 150, 200, 300));

                GetEGLLable("Transition settings: ", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleCenter, 25));
                GetEGLLable("color: ", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleCenter, 25));
                t.Color = EditorGUILayout.ColorField(t.Color);

                GetEGLLable("start position: ", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleCenter, 25));
                t.startPlacement = (EWindowCurvePlacement)EditorGUILayout.EnumPopup(t.startPlacement);

                GetEGLLable("end position: ", GUIStylizer.GetStyle(GUIStylizer.Colors.WHITE, TextAnchor.MiddleCenter, 25));
                t.endPlacement = (EWindowCurvePlacement)EditorGUILayout.EnumPopup(t.endPlacement);


                GUILayout.EndArea();

                if (t.removable && GUI.Button(new Rect(10, 300, 80, 20), "Remove"))
                {
                    t.startNode.AddTransitionsToRemove(t.ID);
                }
            }
        }
        public void MakeTransition(BaseNode start, BaseNode end)
        {
            if (start == end) { Debug.Log("Transition doesnt made"); isMakingTransition = false; return; };
            Transition t;
            if (start.drawNode is ConditionNode)
            {
                if (start.transitions.Count < start.drawNode.transitionsLimit)
                {
                    ConditionNode c = start.drawNode as ConditionNode;
                    GenericMenu menu = new GenericMenu();

                    if (!start.transitions.Exists(x => x.Value == "true"))
                        menu.AddItem(new GUIContent("True"), false, delegate { t = new Transition(start, end, EWindowCurvePlacement.RightBottom, EWindowCurvePlacement.LeftCenter, Color.red, false, "true", true,currentGraph.GenerateId()); });
                    if (!start.transitions.Exists(x => x.Value == "false"))
                        menu.AddItem(new GUIContent("False"), false, delegate { t = new Transition(start, end, EWindowCurvePlacement.LeftBottom, EWindowCurvePlacement.LeftCenter, Color.blue, false, "false", true,currentGraph.GenerateId()); });
                    menu.ShowAsContext();
                }
            }
            else
            {
                t = new Transition(start, end, EWindowCurvePlacement.RightCenter, EWindowCurvePlacement.LeftCenter, Color.magenta, false, null, true,currentGraph.GenerateId());
            }
            isMakingTransition = false;
        }
        public static void DrawNodeCurve(Transition t, Rect start, Rect end, EWindowCurvePlacement start_, EWindowCurvePlacement end_, Color curveColor, bool disable)
        {
            Vector3 endPos = Vector3.zero;
            Vector3 startPos = Vector3.zero;
            switch (start_)
            {
                case EWindowCurvePlacement.LeftTop:
                    startPos = new Vector3(start.x + 1, start.y + 3, 0);

                    break;
                case EWindowCurvePlacement.LeftBottom:
                    startPos = new Vector3(start.x + 1, start.y + start.height - 3, 0);
                    break;
                case EWindowCurvePlacement.CenterBottom:
                    startPos = new Vector3(start.x + (start.width * 0.5f), start.y + start.height - 2, 0);

                    break;
                case EWindowCurvePlacement.CenterTop:
                    startPos = new Vector3(start.x + (start.width * 0.5f), start.y + 2, 0);
                    break;
                case EWindowCurvePlacement.RightTop:
                    startPos = new Vector3(start.x + start.width, start.y + 3, 0);
                    break;
                case EWindowCurvePlacement.RightBottom:
                    startPos = new Vector3(start.x + start.width, start.y + start.height - 3, 0);
                    break;
                case EWindowCurvePlacement.RightCenter:
                    startPos = new Vector3(start.x + start.width, start.y + (start.height * 0.5f), 0);
                    break;
                case EWindowCurvePlacement.LeftCenter:
                    startPos = new Vector3(start.x, start.y + (start.height * 0.5f), 0);

                    break;
                case EWindowCurvePlacement.Center:
                    startPos = new Vector3(start.x + (start.width * 0.5f), start.y + (start.height * 0.5f), 0);
                    break;

            }
            switch (end_)
            {
                case EWindowCurvePlacement.LeftTop:
                    endPos = new Vector3(end.x + 1, end.y + 3, 0);

                    break;
                case EWindowCurvePlacement.LeftBottom:
                    endPos = new Vector3(end.x + 1, end.y + end.height - 3, 0);
                    break;
                case EWindowCurvePlacement.CenterBottom:
                    endPos = new Vector3(end.x + (end.width * 0.5f), end.y + end.height - 2, 0);

                    break;
                case EWindowCurvePlacement.CenterTop:
                    endPos = new Vector3(end.x + (end.width * 0.5f), end.y + 2, 0);
                    break;
                case EWindowCurvePlacement.RightTop:
                    endPos = new Vector3(end.x + end.width, end.y + 3, 0);
                    break;
                case EWindowCurvePlacement.RightBottom:
                    endPos = new Vector3(end.x + end.width, end.y + end.height - 3, 0);
                    break;
                case EWindowCurvePlacement.RightCenter:
                    endPos = new Vector3(end.x + end.width, end.y + (end.height * 0.5f), 0);
                    break;
                case EWindowCurvePlacement.LeftCenter:
                    endPos = new Vector3(end.x, end.y + (end.height * 0.5f), 0);

                    break;
                case EWindowCurvePlacement.Center:
                    endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
                    break;

            }


            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Handles.Label(((startPos + endPos) * .5f) + Vector3.down * -5, t != null ? t.Value : "", GUIStylizer.GetStyle(curveColor, TextAnchor.MiddleCenter, 25));

            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 4);
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, disable ? Color.black : curveColor, null, 3);
            if (t != null && t.enableClickPoint)
                DrawTransitionClickPoint(t, startPos, endPos);
        }
        #endregion
        public static void GetEGLLable(string text, GUIStyle style)
        {
            EditorGUILayout.LabelField(text, style);
        }


      
    }
#endif








}
