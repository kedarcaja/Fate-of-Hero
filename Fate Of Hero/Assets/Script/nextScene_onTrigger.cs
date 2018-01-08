using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextScene_onTrigger : MonoBehaviour {

	public bool teleportTriggered;
	public Texture2D fadeOutTexture;	
	public float fadeSpeed = 0.8f;		
	private int drawDepth = -1000;		
	private float alpha = 1.0f;			
	private int fadeDir = -1;			

	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) {
			print ("collided");
			StartCoroutine (Load());
		}

	}

	void OnGUI()
	{		
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;																
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);		
	}
	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return (fadeSpeed);
	}		
	void OnLevelWasLoaded()
	{		
		BeginFade(-1);	
	}
	IEnumerator Load()
	{
		BeginFade (1);
		yield return new WaitForSeconds(fadeSpeed);
#pragma warning disable CS0618 // Typ nebo člen je zastaralý.
        if (Application.levelCount > Application.loadedLevel)
#pragma warning restore CS0618 // Typ nebo člen je zastaralý.
        {
#pragma warning disable CS0618 // Typ nebo člen je zastaralý.
            Application.LoadLevel (Application.loadedLevel + 1);
#pragma warning restore CS0618 // Typ nebo člen je zastaralý.
        }
	}
}