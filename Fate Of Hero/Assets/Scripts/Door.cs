using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField]
    private float time;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject exit;
    private GameObject Player;
     [Header("Dveře = true, průchod = false")]
    public bool door;
    private bool timer;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (time == 0) { Debug.LogError("<color=Red><b>ERROR: </b> time value is not defined </color>"); }
        if (waitTime == 0) { Debug.LogError("<color=Red><b>ERROR: </b> waitTime value is not defined </color>"); }
        if (!exit) { Debug.LogError("<color=Red><b>ERROR: </b> The exit object was not found </color>"); }
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