using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is one part of subtitles in dialog
/// </summary>
[CreateAssetMenu(fileName = "NewDialPart", menuName = "Dialog/Part")]
public class DialogPart : ScriptableObject
{
    public Character Speaker;
    [TextArea(10,100)]
    public string Subtitles = "";
    public float StartDuration;
  

    public override string ToString()
    {
        return Speaker.ToString() + ": " + Subtitles;
    }
  
}
