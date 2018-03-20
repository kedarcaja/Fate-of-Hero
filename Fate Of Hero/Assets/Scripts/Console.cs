using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Console : MonoBehaviour {
    public Text OutputText;
    public float letterPause = 0.05f;
    public string message;
    
    // Use this for initialization
    void Start () {
        //OutputText = GameObject.Find("LogText").GetComponent<Text>();
        //InputText = GameObject.Find("Computer Input Field Text").GetComponent<Text>();
        //OutputText.text = "\n" + "Hello, this is AAUOS" + "\n" + "Absolutely Awesome Useless Operating System" + "\n" + "For any help just type :help";
		StartCoroutine(Dialog());
    }
	IEnumerator Dialog()
	{
		message = ("Dummy 1: Ahoj, jak se máš?");
		StartCoroutine (TypeText ());
		yield return new WaitForSeconds (3);
		GetComponent<Text> ().text = "";
		message = ("Dummy 2: Mam se fakt suprově!!!");
		StartCoroutine (TypeText ());
		yield return new WaitForSeconds (3);
		GetComponent<Text> ().text = "";
		message = ("Dummy 1: Tohle nemá cenu komentovat, jsme tu jen od toho, aby sme vyzkoušeli dialogovej systém ;(");
		StartCoroutine (TypeText ());
	}

	
    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            OutputText.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
