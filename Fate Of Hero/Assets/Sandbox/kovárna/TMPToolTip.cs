using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TMPToolTip : MonoBehaviour {

    private Text text;
    [SerializeField]
    private GameObject Panel;
    private float max, min;
    private void Awake()
    {
        text = Panel.GetComponentInChildren<Text>();
        max = 200;
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
        text.text = min + "/" + max;
    }
}
