using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public float time;
    public float waitTime;
    public GameObject exit;
    private GameObject Player;
     [Header("Dveře = true, průchod = false")]
    public bool door;
    private bool timer;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (timer)
        {
            if (time >= waitTime)
            {

                Load();
                timer = false;

            }
            else { time += UnityEngine.Time.deltaTime; }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (door == true)
        {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                print("collided");
                //StartCoroutine(load());

            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                print("collided");
                timer = true;
                
            }
        }

    }

    public void Load()
    {
        time = 0;
        Player.transform.position = exit.transform.position;
    }

   
}