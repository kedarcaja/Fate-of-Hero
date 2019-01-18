using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "Dialog/New Speaker")]
public class Speaker : ScriptableObject
{
   
    [SerializeField]
    private Color color;
    private void Awake()
    {
        color.a = 255;
    }
    public string SpeakerName
    {
        get
        {
            return string.Format("<color=#{1}>{0}:</color> ", name, ColorUtility.ToHtmlStringRGBA(color));
        }

    }
  


}
