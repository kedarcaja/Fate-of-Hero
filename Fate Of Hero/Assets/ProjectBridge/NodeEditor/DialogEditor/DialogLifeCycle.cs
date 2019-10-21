using NodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogEditor
{
    [Serializable]
    public class DialogLifeCycle : NodeGraphLifeCycle, IPlayable
    {
        private _Timer timer;

        private DialogGraph dialogGraph;
        private bool isPlaying = false;

        public override void CheckAlwaysConditions()
        {
        }

        public DialogLifeCycle(DialogGraph graph)
        {
            Init(graph);
            dialogGraph = graph;
        }
        public override void Tick()
        {
            if (isPlaying)
            {
                ExecuteSubtitles();
            }
        }
        void ExecuteSubtitles()
        {
            if (!currentNode.executed)
            {
                timer = new _Timer(0.1f, currentNode.dialogPartStartDuration, DialogManager.Instance);
                timer.OnUpdate += () => { currentNode.nodeCompleted = true; };
                timer.Execute();
                currentNode.Execute();
                currentNode.executed = true;
                currentNode.nodeCompleted = false;
            }

            else if (currentNode.nodeCompleted && currentNode.executed)
            {
                DialogManager.Instance.StopAllCoroutines();
                currentNode.executed = false;
                DecideForNextNode();
                currentNode.executed = false;
            }
        }



        public void Play()
        {
            if (isPlaying) return;
            isPlaying = true;

        }


        public void Stop()
        {
            if (!isPlaying) return;
            isPlaying = false;
            DialogManager.Instance.Stop();
        }

        public void Pause()
        {

        }

        public bool IsPlaying()
        {
            return isPlaying;
        }
        public override void DecideForNextNode()
        {
            if (currentNode.transitions.Count != 0)
            {
                base.DecideForNextNode();
            }
            else
            {
                Stop();
            }
        }
    }
}