using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using BehaviourEditor;
using DialogEditor;

namespace NodeEditor
{
    public class NodeGraph : ScriptableObject
    {
        public List<BaseNode> nodes = new List<BaseNode>();
        public List<string> removeNodesIDs = new List<string>();
        protected BaseNode enterNode;
        public List<BaseNode> selectedNodes = new List<BaseNode>();
        public BaseNode EnterNode { get => enterNode; }
        private NodeGraphLifeCycle lifeCycle;

        public virtual NodeGraphLifeCycle LifeCycle()
        {
            return lifeCycle;
        }
        public  string GenerateId()
        {
            System.Random r = new System.Random();
            char[] a = { 'A', 'E', 'C', 'G', 'H', 'T', 'J' };
            return DateTime.Now.Second.ToString() + a[r.Next(0, 7)] + nodes.Count.ToString() + r.Next(1, 100);
        }
        public void RemoveTransitions()
        {
            if(nodes != null)
            foreach (BaseNode b in nodes)
            {
                b?.RemoveTransitions();
            }
        }
        public bool IsEnterState(BaseNode statr, BaseNode b)
        {
            return b.depencies.Exists(d => d.startNode == statr);
        }
        public void SetAsEnterState(BaseNode start, BaseNode end, Color color)
        {

            if (start.transitions.Count > 0 && start.transitions[0].endNode != null)
            {
                start.transitions.Clear();
            }
            start.transitions = new List<Transition>() { new Transition(start, end, EWindowCurvePlacement.RightCenter, EWindowCurvePlacement.LeftCenter, color, false, "", false,GenerateId()) };
        }
        protected virtual void Awake()
        {
#if UNITY_EDITOR
            DialogEditor.DialogEditor.DrawNodes = AssetDatabase.LoadAssetAtPath("Assets/NodeEditor/Resources/DrawNodesHolder.asset", typeof(DrawNodeHolder)) as DrawNodeHolder; ;
            BehaviourEditor.BehaviourEditor.DrawNodes = AssetDatabase.LoadAssetAtPath("Assets/NodeEditor/Resources/DrawNodesHolder.asset", typeof(DrawNodeHolder)) as DrawNodeHolder;
#endif
        }

        public virtual BaseNode AddNode(DrawNode drawNode, float x, float y, string title)
        {
            BaseNode n = new BaseNode(drawNode, x, y, title, GenerateId());
            nodes.Add(n);

            if (enterNode.transitions.Count == 0)
            {
                SetAsEnterState(enterNode, n, Color.green);
            }
            return n;
        }
        protected void OnEnable()
        {
            InitTransitions();
        }


        /// <summary>
        /// Adds nodes id to list of nodes to remove
        /// </summary>
        public void RemoveNodeSelectedNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null) continue;

                nodes.RemoveAll(a => removeNodesIDs.Contains(a.ID));

            }
            removeNodesIDs.Clear();
        }

        /// <summary>
        /// Creates unique id for node
        /// </summary>
        /// <returns></returns>
       

        public void DuplicateSelection(float x, float y)
        {

            foreach (BaseNode b in selectedNodes)
            {
                if (b.drawNode.duplicatable)
                {
                    AddNode(b.drawNode, x, y, b.WindowTitle);
                }
            }
            UnFocusSelectedNodes();
            selectedNodes.Clear();
        }
        /// <summary>
        /// Initializes transitions end and start window after serialization
        /// </summary>
        public void InitTransitions()
        {

            foreach (BaseNode b in nodes)
            {
                if (b == null) continue;
                foreach (Transition t in b.transitions)
                {
                    if ((t == null) || (t.startNode != null && t.endNode != null)) continue;
                    BaseNode start = b;
                    BaseNode end = null;

                    nodes.ForEach(e => e.depencies.ForEach(d => { if (d.ID == t.ID && end == null) { end = e; }; }));

                  

                    t.DrawConnection(start, end, t.startPlacement, t.endPlacement, t.Color, t.disabled);
                }
            }
        }

        public void UnFocusSelectedNodes()
        {
            foreach (BaseNode b in selectedNodes)
            {
                b.focused = false;

                ////////foreach (Transition t in transi)
                ////////{

                ////////}
            }


        }
    }

}
