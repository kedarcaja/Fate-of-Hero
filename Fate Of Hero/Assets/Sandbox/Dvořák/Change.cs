using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour {

	private GameObject first, second;

    private void OnTriggerStay(Collider other)
    {
       
        if (other.tag == "Player")
        {
            first.SetActive(false);
            second.SetActive(true);
        }
    }
    }
