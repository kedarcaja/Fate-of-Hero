using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JezirkoTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(other.tag== "Player"&&!FindObjectOfType<Text>().GetComponent<AudioSource>().isPlaying&&Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Subtitles>().Dialogs[0].trigger = true;
        }
    }
}
