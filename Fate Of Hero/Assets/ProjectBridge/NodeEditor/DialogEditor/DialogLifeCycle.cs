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
            if (IsPlaying())
            {
                if (currentNode != null)
                {
                    ExecuteNodes();
                }

            }
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
        public bool IsPlaying()
        {
            return !isStopped && !isPaused;
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
                    DialogManager.Instance.SubtitleArea.text = "";
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

            else if (currentNode.drawNode is DialogDecisionNode && currentNode.decided && currentNode.nodeCompleted)
            {
                currentNode.Execute();
                currentNode.nodeCompleted = false;
                currentNode.decided = false;
                DecideForNextNode();
                currentNode.executed = false;
            }
            else if (!(currentNode.drawNode is DialogPartNode))
            {
                currentNode.Execute();
                currentNode.nodeCompleted = true;
                DecideForNextNode();
                currentNode.executed = false;

            }


        }

        public override void DecideForNextNode()
        {
            if (currentNode.drawNode is DialogDecisionNode && currentNode.nodeCompleted&& currentNode.decided)
            {
                switch (currentNode.decisionSelectedOption)
                {
                    case 0:
                        currentNode = currentNode.transitions[0].endNode;
                        Debug.Log("Selected option 0");
                        return;

                    case 1:
                        currentNode = currentNode.transitions[1].endNode;
                        Debug.Log("Selected option 1");

                        return;
                    case 2:
                        currentNode = currentNode.transitions[2].endNode;
                        Debug.Log("Selected option 2");

                        return;
                    case 3:
                        currentNode = currentNode.transitions[3].endNode;
                        Debug.Log("Selected option 3");

                        return;
                    case 4:
                        currentNode = currentNode.transitions[4].endNode;
                        Debug.Log("Selected option 4");

                        return;

                }
            }

            base.DecideForNextNode();

        }



        public void Play()
        {
            if (!IsPlaying())
            {
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
            }
        }

        public void Stop()
        {
            if (IsPlaying())
            {

                isStopped = true;
                if (timer != null && timer.IsRunning())
                {
                    timer.Stop();
                }
            }
        }
    }
}