using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class stats
{
    private BS bar;
    #region Varibles
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
            currentVal = value;
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
