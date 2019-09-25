using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum EWindowCurvePlacement { LeftTop, LeftBottom, CenterBottom, CenterTop, RightTop, RightBottom, RightCenter, LeftCenter, Center }

namespace NodeEditor
{
    [Serializable]
    public class Transition
    {
        public string ID = "";
        public Color Color = Color.green;
        [NonSerialized] // stops recusrsive serialization
        public BaseNode startNode, endNode;
        public bool disabled = false;
        public EWindowCurvePlacement startPlacement, endPlacement;
        public bool clicked = false; // vratit na object
        public string Value = "";
        public bool enableClickPoint = true;
        public bool removable = true;
        public Transition(BaseNode start, BaseNode end, EWindowCurvePlacement sPos, EWindowCurvePlacement ePos, Color col, bool disable, string val, bool enableClickPoint, string id)
        {
            if ((start.transitions.Count > 0 && start.transitions.Exists(t => t != null && t.endNode != null && end != null && t.endNode.ID == end.ID)) ||
                start.drawNode.UnconectableNodes.Contains(end.drawNode)) { Debug.Log("Transition doesnt passed through constructor..."); return; }; // A cannot be connected ti B but B can be connected to A

            DrawConnection(start, end, sPos, ePos, col, disable);
            start.transitions.Add(this);
            end.depencies.Add(this);
            this.Value = val;
            this.enableClickPoint = enableClickPoint;
            this.ID = id;
        }
        public void DrawConnection(BaseNode start, BaseNode end, EWindowCurvePlacement sPos, EWindowCurvePlacement ePos, Color col, bool disable)
        {
            Color = col;
            startNode = start;
            endNode = end;
            disabled = disable;
            startPlacement = sPos;
            endPlacement = ePos;

        }
    }
}