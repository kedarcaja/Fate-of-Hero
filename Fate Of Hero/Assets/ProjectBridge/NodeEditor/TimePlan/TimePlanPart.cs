using BehaviourEditor;
using NodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "TimePlan/Part")]

public class TimePlanPart : ScriptableObject
{
    [SerializeField]
    private BehaviourGraph graph;
    [SerializeField]
    private int hours, minutes, seconds;
    [SerializeField]
    private EDays day;
    [SerializeField]
    private bool everyDay;

    public bool EveryDay { get => everyDay; }
    public EDays Day { get => day; }
  public TimeSpan Time { get { return new TimeSpan(hours, minutes, seconds); } }
    public BehaviourGraph Graph { get => graph; }

    public void Change(int hours, int minutes, int seconds, EDays day, bool everyDay, BehaviourGraph graph)
    {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
        this.day = day;
        this.everyDay = everyDay;
        this.graph = graph;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}
