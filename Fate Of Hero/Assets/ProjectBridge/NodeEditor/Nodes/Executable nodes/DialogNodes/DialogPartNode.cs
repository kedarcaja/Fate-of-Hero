using NodeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogEditor
{
    [CreateAssetMenu(menuName = "DialogEditor/Nodes/Part")]
    public class DialogPartNode : ExecutableNode
    {
        [SerializeField]
        private int charLimit = 2000;
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
            if (!b.collapse)
                b.WindowRect.height = b.drawNode.Height + (b.dialogPartSubtitles.Length / 30 * 5f);
            DialogEditor.GetEGLLable("Character: " + (b.dialogPartspeaker != null ? b.dialogPartspeaker.ToString() : ""), GUIStyle.none);
            b.dialogPartspeaker = EditorGUILayout.ObjectField(b.dialogPartspeaker, typeof(Character), false) as Character;

            DialogEditor.GetEGLLable("Duration: ", GUIStyle.none);
            b.dialogPartStartDuration = EditorGUILayout.Slider(b.dialogPartStartDuration, 0.2f, 5);

            DialogEditor.GetEGLLable("Subtitles: ", GUIStyle.none);
            b.dialogPartSubtitles = GUILayout.TextArea(b.dialogPartSubtitles, charLimit);

            EditorUtility.SetDirty(b.DialogGraph);

        }

        public override void Execute(BaseNode b)
        {
            DialogManager.Instance.SubtitleArea.text = b.dialogPartspeaker.ToString() + ": " + b.dialogPartSubtitles;
        }
    }
}