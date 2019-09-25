using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EDecOptionType {Fight,Shop,End,Interaction,Building }
[CreateAssetMenu(menuName ="Dialog/DecisionOption")]
public class DecisionOption : ScriptableObject
{
    [SerializeField]
    private Dialog dialog;
    [TextArea(1,1)]
    [SerializeField]
    private string text;
    [Header("[%]")]
    [SerializeField]
    private float speach;
    [SerializeField]
    private EDecOptionType type;
    [SerializeField]
    private int repeatLimit = 1;
    [SerializeField]
    private bool unlimitedRepeating = false;
    public EDecOptionType Type { get => type; }
    public float Speach { get => speach; }
    public bool Enable { get { return repeatLimit > 0 || unlimitedRepeating; } }

    public string Text { get => text;}
    public Dialog Dialog { get => dialog;  }
}
