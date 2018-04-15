using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour {

    private AudioSource audi;
    public AudioClip Sound;



    private void Awake()
    {
        audi = gameObject.AddComponent<AudioSource>();

        audi.loop = false;
        audi.playOnAwake = false;
    }



    public  void PlaySound()
    {




        audi.PlayOneShot(Sound);


    }
}
