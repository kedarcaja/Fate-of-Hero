using NodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static GUIStylizer;

namespace DialogEditor
{
    [CreateAssetMenu(menuName = "DialogEditor/Graph")]
    public class DialogGraph : NodeGraph, IPlayable
    {

        public BaseNode firstSubtitles;

        #region Dialog variables
        internal bool wasPlayed = false;
        internal bool repeatable = false;
        internal bool skipable = false;
        internal int repeatLimit = 1;
        internal bool unlimitedRepeating = false;
        internal bool IsEnable = true;
        public bool destroyOnEnd = false;
        internal DialogLifeCycle lifeCycle;
        internal int playCount = 0;
        private bool isStopped = true, isPaused = false;
        #endregion

        public override NodeGraphLifeCycle LifeCycle()
        {
            return lifeCycle;
        }

        public override BaseNode AddNode(DrawNode drawNode, float x, float y, string title)
        {
            BaseNode n = base.AddNode(drawNode, x, y, title);
            n.DialogGraph = this;
            if (firstSubtitles.transitions.Count == 0)
            {
                SetAsEnterState(firstSubtitles, n, Colors.ORANGE);
            }
            return n;
        }
        public bool IsFirstSubtitles(BaseNode b)
        {
            return b.depencies.Exists(d => d.startNode == firstSubtitles);
        }
        protected override void Awake()
        {
            base.Awake();
            if (!nodes.Exists(f => f.drawNode is EnterNode))
            {
#if UNITY_EDITOR
                enterNode = new BaseNode(DialogEditor.DrawNodes.EnterNode, 10, 200, "", GenerateId());
#endif
                nodes.Add(enterNode);
                enterNode.DialogGraph = this;

            }
            else
            {
                enterNode = nodes.Find(f => f.drawNode is EnterNode);
            }


            if (!nodes.Exists(f => f.drawNode is DialogNode))
            {
#if UNITY_EDITOR
                firstSubtitles = new BaseNode(DialogEditor.DrawNodes.DialogNode, 200, 200, "Dialog Audio", GenerateId());
#endif
                nodes.Add(firstSubtitles);
                firstSubtitles.DialogGraph = this;

            }
            else
            {
                firstSubtitles = nodes.Find(f => f.drawNode is DialogNode);
            }


            SetAsEnterState(enterNode, firstSubtitles, Colors.GREEN);
        }
        public void RemoveDecisionBranch(BaseNode b)
        {

            for (int i = 0; i < b.transitions.Count; i++)
            {
                if (b.transitions.Count == 0) return;
                BaseNode c = b.transitions[i].endNode;

                removeNodesIDs.Add(c.ID);

                RemoveDecisionBranch(c);
            }
        }
        public void AddDecisionBranch(BaseNode decisionNode, float x, float y)
        {

            string id0 = GenerateId() + 0 + "u", id1 = GenerateId() + 1 + "u", id2 = GenerateId() + 2 + "u"; // modify id because bug generates same id everytime

#if UNITY_EDITOR
            BaseNode audio = AddNode(DialogEditor.DrawNodes.DialogNode, x, y, "Dialog Audio");
            BaseNode even = AddNode(DialogEditor.DrawNodes.DialogEventNode, x + 5, y + 300, "Event");
            BaseNode subtitles = AddNode(DialogEditor.DrawNodes.DialogPartNode, x + 10, y + 500, "Dialog Subtitles");

            Transition
             t0 = new Transition(decisionNode, audio, EWindowCurvePlacement.CenterBottom, EWindowCurvePlacement.CenterTop, Colors.REDPING, false, "", false, id0),
             t1 = new Transition(audio, even, EWindowCurvePlacement.CenterBottom, EWindowCurvePlacement.CenterTop, Colors.REDPING, false, "", false, id1),
             t2 = new Transition(even, subtitles, EWindowCurvePlacement.CenterBottom, EWindowCurvePlacement.CenterTop, Colors.REDPING, false, "", false, id2);


            t0.removable = false;
            t1.removable = false;
            t2.removable = false;


            even.collapse = true;
            subtitles.collapse = true;
#endif
        }

        public void Skip()
        {
            if (skipable)
            {
                lifeCycle.Skip();
            }
        }
        public void Play()
        {
            if (!IsPlaying())
            {
                if (isStopped)
                {
                    if (!CanPlay()) return;

                    ResetDialog();
                }
                lifeCycle.Play();
                isStopped = false;
                isPaused = false;
            }
        }

        public void Pause()
        {
            if (IsPlaying())
            {
                lifeCycle.Pause();
                isPaused = true;

            }
        }
        public bool IsPlaying()
        {
            return !isPaused && !isStopped && lifeCycle.IsPlaying();
        }
        public void Stop()
        {
            if (IsPlaying())
            {
                lifeCycle.Stop();
                isStopped = true;

            }
        }

        private bool CanPlay()
        {
            return IsEnable ? (unlimitedRepeating ? true : (repeatable ? repeatLimit > playCount : !wasPlayed)) : false;
        }
        public void ResetDialog()
        {
            playCount++;
        }
        public void InitDialog()
        {
            lifeCycle = new DialogLifeCycle(this);
        }
    }


}
