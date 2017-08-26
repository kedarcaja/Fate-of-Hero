using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    
    public bool teleportTriggered;
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.9f;
    private int drawDepth = -1000;
    private float alpha = 0.5f;
    private int fadeDir = -1;
    public GameObject exit;
    public GameObject Player;
     [Header("Dveře = true, průchod = false")]
    public bool door;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (door == true)
        {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                print("collided");
                StartCoroutine(load());
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                print("collided");
                StartCoroutine(load());
            }
        }

    }

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    IEnumerator load()
    {
        BeginFade(1);
        yield return new WaitForSeconds(fadeSpeed);
        Player.transform.position = exit.transform.position;
        BeginFade(-1);
    }
}