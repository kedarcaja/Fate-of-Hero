using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingMap : MonoBehaviour {

    [SerializeField]
    private Text Funkcion;

    //[SerializeField]
    //private string funkcion;

	void Start () {
        Funkcion.text = "";
	}


    public void ShowText(string text)
    {
        Funkcion.text = text;
    }

    public void HideText()
    {
        Funkcion.text = "";

    }
}
