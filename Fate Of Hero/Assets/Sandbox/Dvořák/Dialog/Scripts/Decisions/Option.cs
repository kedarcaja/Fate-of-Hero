using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Option", fileName = "Option")]
public class Option : ScriptableObject
{
    [SerializeField]
    private Dialogs dialoAfter;
    [TextAreaAttribute(20, 100)]
    [SerializeField]
    private string sentence;
    [SerializeField]
    private Sprite icon;

    public string Sentence { get { return sentence; } }
    public Dialogs DialoAfter { get { return dialoAfter; } }
    public Sprite Icon { get { return icon; } }
    public DialogHandler OnDecision;
}
