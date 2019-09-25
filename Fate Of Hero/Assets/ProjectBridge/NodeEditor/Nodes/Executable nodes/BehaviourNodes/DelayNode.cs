using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Delay")]
    public class DelayNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode b)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR


            EditorGUILayout.LabelField("delay:(float)/s-1");
            b.delay = EditorGUILayout.FloatField(b.delay);
            if (b.delay < 1) b.delay = 1;
#endif
        }

        public override void Execute(BaseNode b)
        {
            if(b.timer == null)
            {
                b.timer = new _Timer(1.0f,1.0f,b.BehaviourGraph.character);

                b.timer.OnUpdate += () =>
                {
                    if (b.timer.ElapsedTimeF == b.delay)
                    {
                        b.nodeCompleted = true;
                        b.timer.Stop();
                        b.timer = null;

                    }

                };
                b.timer.Execute();
            }
        }
    }
}