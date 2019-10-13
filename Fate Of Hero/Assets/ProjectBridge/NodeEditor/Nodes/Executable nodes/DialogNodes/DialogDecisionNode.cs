using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogEditor
{
    [CreateAssetMenu(menuName = "DialogEditor/Nodes/Decision")]
    public class DialogDecisionNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
            GUIStyle s = new GUIStyle();
            s.fontSize = 20;
            s.richText = true;
            s.alignment = TextAnchor.UpperCenter;
            s.padding.top = 10;
            s.normal.textColor = GUIStylizer.Colors.LIGHTSKYBLUE;
#if UNITY_EDITOR
            DialogEditor.GetEGLLable("Decision", s);
#endif
        }

        public override void Execute(BaseNode b)
        {
            if (b.transitions.Count == 0) return;

            string[] data = new string[b.transitions.Count];


            for (int i = 0; i < b.transitions.Count; i++)
            {
                BaseNode subs = b.transitions[i].endNode.transitions[0].endNode.transitions[0].endNode;

                data[i] = subs.dialogPartSubtitles;
            }
            //DialogManager.Instance.DecisionOptions.SetGroup(data);
            //if (DialogManager.Instance.DecisionOptions.lastSelectedButtonIndex < 5)
            //{
            //    b.decided = true;
            //    b.decisionSelectedOption = DialogManager.Instance.DecisionOptions.lastSelectedButtonIndex;
            //    b.nodeCompleted = true;
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha1) && (b.transitions.Count >= 1))
            //{
            //    b.decisionSelectedOption = 0;
            //    b.decided = true;

            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha2) && (b.transitions.Count >= 2))
            //{
            //    b.decisionSelectedOption = 1;
            //    b.decided = true;
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha3) && (b.transitions.Count >= 3))
            //{
            //    b.decisionSelectedOption = 2;
            //    b.decided = true;
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha4) && (b.transitions.Count >= 4))
            //{
            //    b.decisionSelectedOption = 3;
            //    b.decided = true;
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha5) && (b.transitions.Count >= 5))
            //{
            //    b.decisionSelectedOption = 4;
            //    b.decided = true;
            //}

        }
    }
}
