using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Dialog/Decision")]
public class Decision : ScriptableObject
{
    [SerializeField]
    private List<DecisionOption> options = new List<DecisionOption>();
    public bool decided { get; set; } = false;
    public List<DecisionOption> Options { get => options;}
    public DecisionOption Selected { get; set; }

}
