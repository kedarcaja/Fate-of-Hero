using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hlaška : MonoBehaviour {

    //public dropdown
    public enum PartList { None,Pryč,Zebou,kámen,Nejdu,lebka,Kupa,Zamčeno,Nora }
    [Header("Výběr Hlášky")]
    [Tooltip("Z menu mužeme vybrat o jaký dialog se jedná")]
    public PartList VybranýPart = PartList.None;
    [Header("Objekty")]
    public GameObject dialogControler;
    public GameObject DialogObject;
    [Header("Volba")]
    public Text DialogText;
    private int Part, Step;
    private float timer, waitTime;
    public AudioClip Voice;
    bool IsPlay;
    public bool Trigger;
   

    private void Start()
    {

       
         if (VybranýPart == PartList.None) { Debug.LogError("<color=Red><b>ERROR: </b> Zapoměl jsi vybrat o jaký dialog se jedná </color>"); }
    }

    void Update()
    {
        if (timer >= waitTime)
        {
            Part = Step; Monolog();

        }
        else { timer += UnityEngine.Time.deltaTime; }

        if (Trigger)
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
        if (VybranýPart == PartList.Pryč)
        {

            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "Jak se odsud dostanu pryč?";
                waitTime = 2f;
                Step = 2;
            }
            else if (Part == 2)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }

        }
        if (VybranýPart == PartList.Zebou)
        {

            if (Part == 1)
            {
                DialogText.text = "Už chci pryč, zebou mě nohy.";
                waitTime = 2f;
                Step = 2;
            }
            else if (Part == 2)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }
        }
        if (VybranýPart == PartList.kámen)
        {
            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "Na tom kameni se něco hýbe.";
                waitTime = 2f;
                Step = 2;
            }
           else if (Part == 2)
            {
                timer = 0;
                DialogText.text = "Jé to je ale hezký drobeček.";
                waitTime = 3f;
                Step = 3;
            }
            else if (Part == 3)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }
        }
        if (VybranýPart == PartList.Nejdu)
        {

            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "K ní teď nejdu, kdybych přišel s prázdnou mohlo by jí to rozrušit. ";
                waitTime = 4.47f;
                Step = 2;
            }
            else if (Part == 2)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }

        }
        if (VybranýPart == PartList.lebka)
        {
            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "Fuj, ta lebka mě děsí.";
                waitTime = 2f;
                Step = 2;
            }
            else if (Part == 2)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }
        }
        if (VybranýPart == PartList.Zamčeno)
        {
            if (Part == 1)
            {
                timer = 0;
                DialogText.text = "Sakra zamčeno.";
                waitTime = 1.72f;
                Step = 2;
            }
            else if (Part == 2)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }
        }
        if (VybranýPart == PartList.Nora)
        {
            if (Part == 1)
            {
                DialogText.text = "Upřímně nevím, zda se mi tam chce.";
                waitTime =4f;
                Step = 2;
            }        
            else if (Part == 2)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }
        }
        if (VybranýPart == PartList.Kupa)
        {
            if (Part == 1)
            {
                DialogText.text = "Aha, kupa sena. Třeba tu bude někde jehla. Prohledám ji. ";
                waitTime = 7f;
                Step = 2;
            }
            else if (Part == 2)
            {
                timer = 0;
                DialogText.text = "no tak zase nic.";
                waitTime = 2f;
                Step = 4;
            }
            if (Part == 4)
            {
                dialogControler.SetActive(false);
                Destroy(DialogObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Trigger = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Trigger = false;
        }
    }
}