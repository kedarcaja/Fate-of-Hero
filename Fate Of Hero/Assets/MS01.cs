using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MS01 : MonoBehaviour {

    public GameObject dialogControler;
    public GameObject DialogObject;
    [Header("Volba")]
    public Text DialogText;
    private int Part, Step;
    private float timer, waitTime;
    public AudioClip Voice;
    bool IsPlay;
    bool Active;
   
    private void Start()
    {
        Active = true;
    }
    void Update()
    {
        if (timer >= waitTime)
        {
            Part = Step; Monolog();

        }
        else { timer += UnityEngine.Time.deltaTime; }

        if (Active)
        {
            if (IsPlay == false)
            {
                IsPlay = true;
                dialogControler.SetActive(true);
                Part = 1; Monolog();
                AudioSource.PlayClipAtPoint(Voice, transform.position, 0.8f);
            }
        }
    }
    public void Monolog()
    {

            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "Kam jsem to sakra zase žuchnul?";
                waitTime = 3f;
                Step = 2;
            }
            else if (Part == 2)
            {
                timer = 0;
                DialogText.text = "A kdo to tu pořád tak brečí?";
                waitTime = 2f;
                Step = 3;
             }
            else if (Part == 3)
            {
            timer = 0;
            DialogText.text = "Možná bych se tu mohl po-rozhlédnout";
            waitTime = 3f;
            Step = 4;
            }
        else if (Part == 4)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }

    }   
    }

   
   
