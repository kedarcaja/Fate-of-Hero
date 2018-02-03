using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip levelMusic;
    public static bool MusicEnd;
    private AudioSource audioSource;
    

    void Awake() {
		DontDestroyOnLoad (gameObject);
		
	}
    private void Update()
    {
        if (MusicEnd == true)
        {
            Destroy(gameObject);
        }
       
      
    }
    void Start () {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = levelMusic;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
	
	
}
