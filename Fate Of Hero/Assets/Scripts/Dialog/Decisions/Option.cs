using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Option",menuName = "Dialog/New Option")]
public class Option : ScriptableObject
{
    [SerializeField]
    private Dialog dialoAfter;
    [TextAreaAttribute(20, 100)]
    [SerializeField]
    private string sentence;
    [SerializeField]
    private Sprite icon;

    public string Sentence { get { return sentence; } }
    public Dialog DialoAfter { get { return dialoAfter; } }
    public Sprite Icon { get { return icon; } }
    public DialogHandler OnDecision;
}
