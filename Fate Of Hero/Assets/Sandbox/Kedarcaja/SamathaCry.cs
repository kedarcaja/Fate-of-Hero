using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamathaCry : MonoBehaviour {

    private AudioSource audioSource;

    private bool Trigger;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
       
        if (Trigger && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Player" )
        {
            Trigger = true;
            

        }
    }
}
