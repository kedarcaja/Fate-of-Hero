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
        [SerializeField]
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

                nodes.Add(enterNode);
                enterNode.DialogGraph = this;
#endif
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


     
        public void Play()
        {
            if (!IsPlaying() && CanPlay())
            {
                if (isStopped)
                {
                    if (!CanPlay()) return;

                    ResetDialog();
                }
                isStopped = false;
                isPaused = false;
            }
        }

        public void Pause()
        {
            if (IsPlaying())
            {
                isPaused = true;
            }
        }
        public bool IsPlaying()
        {
            return !isPaused && !isStopped;
        }
        public void Stop()
        {
          //  wasPlayed = true;
            isStopped = true;
        }

        private bool CanPlay()
        {
            return IsEnable;
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
