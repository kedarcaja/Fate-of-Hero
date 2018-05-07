using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MS01Controler : MonoBehaviour {

    [Header("Objekty")]
    [SerializeField]
    private GameObject dialogControler;
    [SerializeField]
    private GameObject dialogObject;
    [Header("Volba")]
    [SerializeField]
    private Text dialogText;
    private int part, step;
    private float timer, waitTime;
    [SerializeField]
    private AudioClip voice;
    bool IsPlay;
    bool Active;
   
    private void Start()
    {
        Active = true;
        if (!dialogControler) { Debug.LogError("<color=Red><b>ERROR: </b> The dialogControler object was not found </color>"); }
        if (!dialogText) { Debug.LogError("<color=Red><b>ERROR: </b> Text dialogText was not found </color>"); }
        if (!dialogObject) { Debug.LogError("<color=Red><b>ERROR: </b> The dialogObject object was not found </color>"); }
        if (!voice) { Debug.LogError("<color=Red><b>ERROR: </b> The voice object was not found </color>"); }
    }
    void Update()
    {
        if (timer >= waitTime)
        {
            part = step; Monolog();

        }
        else { timer += UnityEngine.Time.deltaTime; }

        if (Active)
        {
            if (IsPlay == false)
            {
                IsPlay = true;
                dialogControler.SetActive(true);
                part = 1; Monolog();
                AudioSource.PlayClipAtPoint(voice, transform.position, 0.8f);
            }
        }
    }
    public void Monolog()
    {

            if (part == 1)
            {
                timer = 0;
                dialogText.text = "Kam jsem to sakra zase žuchnul?";
                waitTime = 3f;
                step = 2;
            }
            else if (part == 2)
            {
                timer = 0;
                dialogText.text = "A kdo to tu pořád tak brečí?";
                waitTime = 2f;
                step = 3;
             }
            else if (part == 3)
            {
            timer = 0;
            dialogText.text = "Možná bych se tu mohl po-rozhlédnout";
            waitTime = 3f;
            step = 4;
            }
        else if (part == 4)
            {
                dialogControler.SetActive(false);
                Destroy(dialogObject);
            }

    }   
    }

   
   
