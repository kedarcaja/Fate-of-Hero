﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private bool monolog;
    private void Update()
    {
               
        if (GetComponent<Subtitles>().Dialogs[0].wasPlayed && !FindObjectOfType<Text>().GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!monolog)
        {
            if (other.tag == "Player" && !FindObjectOfType<Text>().GetComponent<AudioSource>().isPlaying && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Subtitles>().Dialogs[0].trigger = true;
            }
        }
        else if (monolog)
        {
            if (other.tag == "Player" && !FindObjectOfType<Text>().GetComponent<AudioSource>().isPlaying)
            {
                
                GetComponent<Subtitles>().Monologs[0].trigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" )
        {
            Destroy(this);
        }
    }
}
