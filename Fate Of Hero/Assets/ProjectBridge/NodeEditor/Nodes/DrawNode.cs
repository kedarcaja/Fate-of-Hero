using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeEditor
{
    public abstract class DrawNode : ScriptableObject
    {

        public float Width = 200, Height = 200,scaledWidth = 200, collapseHeight = 105;
        public Color NodeColor = Color.grey;
        public bool Deletable = true;
        public int transitionsLimit = 0;
        public bool unlimitedTransitions;
        public bool  enableCollapse = true,duplicatable = true;
        [SerializeField]
        protected List<DrawNode> unconectableNodes = new List<DrawNode>();

        public List<DrawNode> UnconectableNodes { get => unconectableNodes; }

        [SerializeField]
        protected bool enableTransitions = true;
        public bool EnableTransitions { get => enableTransitions; }


        public abstract void DrawWindow(BaseNode node);
        public abstract void DrawCurve(BaseNode node);


    }
}