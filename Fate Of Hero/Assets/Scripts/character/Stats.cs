using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Stats
{ 
    #region Varibles
[SerializeField]
    private BarScript bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            Bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        
	
        get
        {
            return maxVal;
        }

        set
        {
           
                maxVal = value;
            Bar.MaxValue = maxVal;
        }
    }

    public BarScript Bar
    {
        get
        {
            return bar;
        }
    }
    #endregion

    #region Unity methods

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
    #endregion
}
