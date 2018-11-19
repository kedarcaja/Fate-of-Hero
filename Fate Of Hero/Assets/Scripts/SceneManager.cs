using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    [Header("Help")]
    [SerializeField]
    private Text sizeTextObject;
    [SerializeField]
    private Text visialTextObject;
    [SerializeField]
    private Text descriptionText;

    
    public GameObject HelpObject;

    public RectTransform rect;

    public Text VisialTextObject
    {
        get
        {
            return visialTextObject;
        }

        set
        {
            visialTextObject = value;
        }
    }

    public Text DescriptionText
    {
        get
        {
            return descriptionText;
        }

        set
        {
            descriptionText = value;
        }
    }

   

    public void ShowHelp()
    {
       
        descriptionText.transform.position = new Vector2(descriptionText.transform.position.x+sizeTextObject.text.Length, descriptionText.transform.position.y);
        sizeTextObject.text = " " + visialTextObject.text + " ";
        HelpObject.SetActive(true); 
    }

    public void HideHelp()
    {
        HelpObject.SetActive(false);
    }
}
