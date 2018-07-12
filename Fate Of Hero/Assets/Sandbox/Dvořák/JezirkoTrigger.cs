using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JezirkoTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(other.tag== "Player")
        {
            GetComponent<Subtitles>().Dialogs[0].trigger = true;
        }
    }
}
