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


        private bool isStopped = true, isPaused = false;
        public override void CheckAlwaysConditions()
        {
        }

        public DialogLifeCycle(DialogGraph graph)
        {
            Init(graph);
        }
        public override void Tick()
        {
            if (IsPlaying() && !Ended())
            {
                if (currentNode != null)
                {
                    ExecuteNodes();
                }
            }
            else
            {
                Stop();
                DialogManager.Instance.Stop();
            }


            Debug.Log(IsPlaying());
        }

        public void Skip()
        {
            Pause();
            while (currentNode.transitions.Count > 0)
            {
                currentNode = currentNode.transitions[0].endNode;
                if (currentNode.drawNode is DialogDecisionNode)
                {
                    Play();
                    return;
                }
            }
            Stop();
        }

        private void ExecuteSubtitles()
        {
            currentNode.Execute();

            timer = new _Timer(0.1f, 0.1f, DialogManager.Instance);
            timer.OnUpdate += delegate
            {
                if (timer.ElapsedTimeF >= currentNode.dialogPartStartDuration)
                {

                    currentNode.nodeCompleted = true;
                    if (currentNode.transitions.Count > 0)
                        DecideForNextNode();

                    timer.Stop();
                }
            };
            timer.Execute();
        }
        private void ExecuteNodes()
        {
            if (currentNode.drawNode is DialogPartNode && (timer == null || !timer.IsRunning()))
            {
                ExecuteSubtitles();
            }

            else if (!(currentNode.drawNode is DialogPartNode))
            {
                currentNode.Execute();
                currentNode.nodeCompleted = true;
                DecideForNextNode();
                currentNode.executed = false;
            }

        }

        public void Play()
        {
            if (!IsPlaying())
            {
                (graph as DialogGraph).Play();
                isStopped = false;
                isPaused = false;
                if (timer != null && !timer.IsPaused)
                {
                    timer.Execute();
                }
            }
        }

        public void Pause()
        {
            if (IsPlaying())
            {
                isPaused = true;
                if (timer != null && timer.IsRunning())
                {
                    timer.Pause();
                }
                (graph as DialogGraph).Pause();
            }
        }
        public bool IsPlaying()
        {
            return !isStopped && !isPaused && (graph as DialogGraph).IsPlaying();
        }
        private bool Ended()
        {
            return currentNode == null || (currentNode.nodeCompleted && currentNode == graph.nodes.Last());
        }
        public void Stop()
        {
            isStopped = true;
            if (timer != null && timer.IsRunning())
            {
                timer.Stop();
            }
            (graph as DialogGraph).Stop();
        }
    }
}