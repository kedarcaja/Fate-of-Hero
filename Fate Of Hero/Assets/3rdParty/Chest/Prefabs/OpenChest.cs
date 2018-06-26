using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {

    private Animator animator;
    private bool isOpen, trigger;
	
	void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && trigger )
        {
            //if (!isOpen)
            //{
            //    isOpen = true;
            //}
            //else
            //{
            //    isOpen = false;
            //}

            if (!isOpen)
            {
               animator.SetBool("Open", true);
                isOpen = true;
            }
           else if  (isOpen)
            {
                animator.SetBool("Open", false);
                isOpen = false;
            }

        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = false;
            animator.SetBool("Open", false);
            isOpen = false;
        }
    }
}
