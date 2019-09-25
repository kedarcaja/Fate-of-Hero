using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimePlan/Plan")]
public class EntityTimePlan : ScriptableObject
{
    [SerializeField]
    private List<TimePlanPart> parts = new List<TimePlanPart>();
    public List<TimePlanPart> Parts { get => parts; }

    public void Remove(TimePlanPart part)
    {
        parts.Remove(part);
    }
    public void Add(TimePlanPart part)
    {
        parts.Add(part);
    }
    public TimePlanPart CurrentPart
    {
        get { if (CurrentPartExists()) return parts.Find(p => (p.Day == MainOpossum.GetDay() || p.EveryDay) && p.Time == MainOpossum.GetTime());
            else return parts.Find(p => (p.Day == MainOpossum.GetDay() || p.EveryDay) && p.Time < MainOpossum.GetTime());         }
    }
    private bool CurrentPartExists()
    {
        return parts.Exists(p => (p.Day == MainOpossum.GetDay() || p.EveryDay) && p.Time == MainOpossum.GetTime());
    }
}
