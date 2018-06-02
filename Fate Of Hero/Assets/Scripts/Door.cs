using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField]
    private GameObject exit;
    public GameObject Player;
     [Header("Dveře = true, průchod = false")]
    public bool door;
    bool indik;

    void Start()
    {
        if (!exit) { Debug.LogError("<color=Red><b>ERROR: </b> The exit object was not found </color>"); }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && indik == true)
        {

            print("true");
            Player.transform.position = exit.transform.position;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
       
         if (door == true)
        {
            if (other.gameObject.tag == "Leonard")
            {
                indik = true;  
            }
            else
            {
                indik = false;
            }
        }
        else
        {
            if (other.gameObject.tag == "Leonard")
            {
                Player.transform.position = exit.transform.position;

            }
        }
    }
   
}