using BehaviourEditor;
using DialogEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeEditor
{
    [CreateAssetMenu(menuName = "NodeEditor/DrawNodesHolder")]
    public class DrawNodeHolder : ScriptableObject
    {
        #region Main Nodes
        [Header("Main Nodes")]
        public CommentNode CommentNode;
        public ConditionNode ConditionNode;
        public CheckAlwaysNode CheckAlwaysNode;
        public DelayNode DelayNode;
        public PortalNode PortalNode;
        public GraphSwapNode GraphSwapNode;
        public EnterNode EnterNode;
        #endregion
        [Space]
        #region Behaviour nodes
        [Header("Behaviour Nodes")]
        public AnimatorSwapNode AnimatorSwapNode;
        public RandomMoveNode RandomMoveNode;
        public AnimatorHandleNode AnimatorHandleNode;
        public SetDestinationNode SetDestinationNode;
        public StopNode StopNode;
        #endregion
        [Space]
        [Header("Dialog Nodes")]
        #region Dialog Nodes
        public DialogPartNode DialogPartNode;
        public DialogNode DialogNode;
        public DialogDecisionNode DialogDecisionNode;
        public DialogEventNode DialogEventNode;
        #endregion
    }
}