using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologScript : MonoBehaviour {

    //public dropdown
    public enum PartList { None,P01,P02,P03,P04,P05,P06,P07,P08 }
    [Header("Dialog selection")]
    [Tooltip("From the menu, we can select what the dialog is about")]
    [SerializeField]
    private PartList selectedPart = PartList.None;
    [Header("Object")]
    [SerializeField]
    private GameObject partControler;
    [SerializeField]
    private GameObject partObject;
    [Header("Choice")]
    [SerializeField]
    private Text partText;
    private int part, step;
    private float timer, waitTime;
    [Header("Sound track")]
    [SerializeField]
    private AudioClip voice;
    bool IsPlay;
    public bool Trigger;
   

    private void Start()
    {
         if (selectedPart == PartList.None) { Debug.LogError("<color=Red><b>ERROR: </b> Zapoměl jsi vybrat o jaký dialog se jedná </color>"); }
       
        if (!partControler) { Debug.LogError("<color=Red><b>ERROR: </b> The partControler object was not found </color>"); }
        if (!partText) { Debug.LogError("<color=Red><b>ERROR: </b> Text partText was not found </color>"); }
        if (!partObject) { Debug.LogError("<color=Red><b>ERROR: </b> The partObject object was not found </color>"); }
        if (!voice) { Debug.LogError("<color=Red><b>ERROR: </b> The voice object was not found </color>"); }
    }

    void Update()
    {
        if (timer >= waitTime)
        {
            part = step; Monolog();

        }
        else { timer += UnityEngine.Time.deltaTime; }

        if (Trigger)
        {
            if (IsPlay == false)
            {
                IsPlay = true;
                partControler.SetActive(true);
                part = 1; Monolog();
                AudioSource.PlayClipAtPoint(voice, transform.position, 0.8f);
            }
        }
    }

    public void Monolog()
    {
        if (selectedPart == PartList.P01)
        {

            if (part == 1)
            {
                timer = 0;
                partText.text = "Jak se odsud dostanu pryč?";
                waitTime = 2f;
                step = 2;
            }
            else if (part == 2)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }

        }
        if (selectedPart == PartList.P02)
        {

            if (part == 1)
            {
                partText.text = "Už chci pryč, zebou mě nohy.";
                waitTime = 2f;
                step = 2;
            }
            else if (part == 2)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }
        }
        if (selectedPart == PartList.P03)
        {
            if (part == 1)
            {
                timer = 0;
                partText.text = "Na tom kameni se něco hýbe.";
                waitTime = 2f;
                step = 2;
            }
           else if (part == 2)
            {
                timer = 0;
                partText.text = "Jé to je ale hezký drobeček.";
                waitTime = 3f;
                step = 3;
            }
            else if (part == 3)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }
        }
        if (selectedPart == PartList.P04)
        {

            if (part == 1)
            {
                timer = 0;
                partText.text = "K ní teď nejdu, kdybych přišel s prázdnou mohlo by jí to rozrušit. ";
                waitTime = 4.47f;
                step = 2;
            }
            else if (part == 2)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }

        }
        if (selectedPart == PartList.P05)
        {
            if (part == 1)
            {
                timer = 0;
                partText.text = "Fuj, ta lebka mě děsí.";
                waitTime = 2f;
                step = 2;
            }
            else if (part == 2)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }
        }
        if (selectedPart == PartList.P06)
        {
            if (part == 1)
            {
                timer = 0;
                partText.text = "Sakra zamčeno.";
                waitTime = 1.72f;
                step = 2;
            }
            else if (part == 2)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }
        }
        if (selectedPart == PartList.P07)
        {
            if (part == 1)
            {
                partText.text = "Upřímně nevím, zda se mi tam chce.";
                waitTime =4f;
                step = 2;
            }        
            else if (part == 2)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }
        }
        if (selectedPart == PartList.P08)
        {
            if (part == 1)
            {
                partText.text = "Aha, kupa sena. Třeba tu bude někde jehla. Prohledám ji. ";
                waitTime = 7f;
                step = 2;
            }
            else if (part == 2)
            {
                timer = 0;
                partText.text = "no tak zase nic.";
                waitTime = 2f;
                step = 4;
            }
            if (part == 4)
            {
                partControler.SetActive(false);
                Destroy(partObject);
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Leonard")
        {
            Trigger = true;
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Leonard")
        {
            Trigger = false;
        }
    }
}