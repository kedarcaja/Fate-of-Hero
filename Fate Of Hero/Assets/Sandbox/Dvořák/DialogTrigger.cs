using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private bool monolog;
    //[SerializeField]
    //private GameObject text;
  
    private void OnTriggerStay(Collider other)
    {
        if (!monolog)
        {
            if (other.tag == "Player" && !GameObject.Find("SubtitlesText").GetComponent<AudioSource>().isPlaying && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Subtitles>().Dialogs[0].trigger = true;
				
			}
			
		}
      
    }
	private void OnTriggerEnter(Collider other)
	{
		   if (monolog)
		{
			if (other.tag == "Player" && !GameObject.Find("SubtitlesText").GetComponent<AudioSource>().isPlaying)
			{

				GetComponent<Subtitles>().Monologs[0].trigger = true;
			}
		}
	}


}
