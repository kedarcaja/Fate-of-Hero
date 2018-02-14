using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BS : MonoBehaviour {
    #region Varibles
    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private float leapSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private bool lerpColor;

    public float MaxValue { get; set; }

    #endregion

    #region Unity methods
    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ":" + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Start () {
        if (lerpColor)
        {
            content.color = fullColor;
        }
	}
    
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * leapSpeed);
        }
        if (lerpColor)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
   
   #endregion
}
