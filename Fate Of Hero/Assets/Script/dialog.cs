using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialog : MonoBehaviour {

	public float letterPause = 0.2f;

	public string message;
	Text textComp;

	// Use this for initialization
	void Start () {
		textComp = GetComponent<Text>();
		message = textComp.text;
		textComp.text = "AHOJ";
		StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			textComp.text += letter;

				
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}
	}
}