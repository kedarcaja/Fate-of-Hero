using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private bool monolog;
	private PlayerController player;

	private void Start()
	{
		player = FindObjectOfType<PlayerController>();

	}
	private void OnTriggerStay(Collider other)
    {
        if (!monolog)
        {
            if (other.tag == "Player" && !GameObject.Find("SubtitlesText").GetComponent<AudioSource>().isPlaying && Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Subtitles>().Dialogs[0].trigger = true;
				player.IsMove = false;

			}

		}
      
    }
	private void OnTriggerEnter(Collider other)
	{
		   if (monolog)
		{
			if (other.tag == "Player" && !GameObject.Find("SubtitlesText").GetComponent<AudioSource>().isPlaying)
			{
				player.IsMove = false;

				GetComponent<Subtitles>().Monologs[0].trigger = true;
			}
		}
	}


}
