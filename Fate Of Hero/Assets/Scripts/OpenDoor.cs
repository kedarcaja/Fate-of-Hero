using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;

    
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Open");
           
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            anim.SetTrigger("Close");
           
        }
    }
}
