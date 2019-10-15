using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyNPC
{
    float HearRadius { get; set; }
    bool CanSwim { get; }
    bool CanDive { get; }

    //EntityTimePlan TimePlan { get; set; }

}
