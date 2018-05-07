using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMPToolTip : MonoBehaviour {

    private Text text;
    [SerializeField]
    private GameObject Panel;
    [HideInInspector]
public  float CurrentValue;
    [SerializeField]
   private float maxValue;

    public float Max
    {
        get
        {
            return maxValue;
        }
    }

    private void Awake()
    {
        text = Panel.GetComponentInChildren<Text>();
    }
    private void OnMouseEnter()
    {
        Panel.SetActive(true);    
    }
    private void OnMouseExit()
    {
        Panel.SetActive(false);
    }
    private void Update()
    {
        text.text =  CurrentValue+ "/" + maxValue;
    }
}
