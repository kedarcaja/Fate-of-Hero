using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogoure", menuName = "Dialog/New Dialogoure")]

public class Dialogoure : ScriptableObject
{
    [SerializeField]
    private Speaker speaker;
    public Speaker Speaker { get { return speaker; } }

    [Range(2, 35)]
    [SerializeField]
    private float startTimer;

    public float Time { get; set; }
    [SerializeField]
    [TextAreaAttribute(0, 50)]
    private string text;
    public string Text { get { return text; } }

    public float StartTimer
    {
        get
        {
            return startTimer;
        }
    }
}
