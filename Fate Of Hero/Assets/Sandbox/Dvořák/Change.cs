using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour {
    [SerializeField]
	private GameObject  second;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Player")
        {
          
            second.SetActive(true);
        }
    }
    }
