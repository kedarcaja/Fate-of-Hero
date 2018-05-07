using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TempChoice : MonoBehaviour
{

[SerializeField]
    private Dropdown TemplateChoose;
    [SerializeField]
    private Sprite[] templates;
   
    private void Start()
    {
        TemplateChoose = FindObjectOfType<Dropdown>();
    }
    private void Update()
    {
        if (TemplateChoose.value == 0)
        {
            gameObject.GetComponent<Image>().color = Color.black;
        }
        else
            gameObject.GetComponent<Image>().color = Color.white;
        gameObject.GetComponent<Image>().sprite = templates[TemplateChoose.value];
    }
}
