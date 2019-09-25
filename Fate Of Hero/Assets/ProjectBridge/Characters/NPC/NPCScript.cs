using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : EntityScript
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {

        base.Update();

        if ((characterData as NPC).TimePlan.CurrentPart?.Graph != null && (characterData as NPC).TimePlan.CurrentPart?.Graph != currentGraph)
        {
            currentGraph = (characterData as NPC).TimePlan.CurrentPart.Graph;
            InitGraph();
        }
    }
}
