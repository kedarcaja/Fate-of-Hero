using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Subtitles : MonoBehaviour
{
	private PlayerController player;
    #region Class Variables
    private static AudioSource audioSource;

    [HideInInspector]
    public static AudioSource Audi
    {
        get
        {
            return audioSource;
        }

    }
    private static Text txt;
    [HideInInspector]
    public static Text Txt
    {
        get { return txt; }
    }
    public Dialog[] Dialogs;
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    public Monolog[] Monologs;



    #endregion

    private void Start()
    {
		txt = GameObject.Find("SubtitlesText").GetComponent<Text>();

		audioSource = txt.GetComponent<AudioSource>();
		player = FindObjectOfType<PlayerController>();

    }
   

    private void Update()
    {
      
        #region Dialog Methods
        for (int i = 0; i < Dialogs.Length; i++)
        {
            if (Dialogs[i].trigger)
            {
                Dialogs[i].StartDialog();
                Dialogs[i].trigger = false;
                StartCoroutine(Dialogs[i].StartTimer());

            }
        }

        #endregion
        #region Monolog Methods
        for (int i = 0; i < Monologs.Length; i++)
        {
            Monologs[i].StartMonolog();

			
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = null;
            txt.text = "";
            //FindObjectOfType<PlayerController>().IsMove = true;

        }
        if (audioSource.isPlaying)
        {
           // FindObjectOfType<PlayerController>().IsMove = false;
        }

		#endregion
		foreach (Monolog m in Monologs)
		{
			if (Input.GetKeyDown(KeyCode.Space) && audioSource.clip == m.clip || m.wasPlayed && !audioSource.isPlaying)
			{
				End();
			}
		}
		foreach (Dialog d in Dialogs)
		{
			if (Input.GetKeyDown(KeyCode.Space)&&audioSource.clip==d.clip || d.wasPlayed && !audioSource.isPlaying)
			{
				End();
			}
		}
		
    }
	public void End()
	{
	
			audioSource.Stop();
			audioSource.clip = null;
			txt.text = "";
			Destroy(gameObject);
				player.IsMove = true;

	}
}





#region Dialog

[Serializable]
        public class Dialog
  {
    public bool wasPlayed;
    public string dialogName;
    public AudioClip clip;
    public bool trigger;
    [Space]
    [Space]
    [SerializeField]
    private DialogSentences[] sentences;
    private DialogSentences currentSentence;
   
            private int sentenceIndex;

    public void StartDialog()
    {
        if (trigger&&!Subtitles.Audi.clip==clip)
        {
            sentenceIndex = 0;
            wasPlayed = true;

            currentSentence = sentences[sentenceIndex];

         
            Subtitles.Audi.clip = clip;
            Subtitles.Audi.Play();
            Subtitles.Txt.text = "<b>" + currentSentence.speaker + ": " + "</b>" + currentSentence.sentence;
            currentSentence = sentences[sentenceIndex];

        }
        
    }
    public IEnumerator StartTimer()
    {
        while (Subtitles.Audi.clip == clip)
        {
            yield return new WaitForSecondsRealtime(1);
          
                currentSentence.timer--;



            if (currentSentence.timer ==0&&sentenceIndex<sentences.Length-1) {
                sentenceIndex++;
                currentSentence = sentences[sentenceIndex];
                Subtitles.Txt.text = "<b>" + currentSentence.speaker + ": " + "</b>" + currentSentence.sentence;
            }           
        }         
    }    
}

[Serializable]
public struct DialogSentences
{

    [TextAreaAttribute (5, 3000)]
    public string sentence;


    public string speaker;

    [Range(1f, 20f)]

    public float timer;

}
#endregion
#region Monolog

[Serializable]
        public struct Monolog
        {
            #region Monolog Variables
        
            public string monologName;
            public bool trigger,wasPlayed;
	
 
            [Space]
            [Space]


            [Multiline]
            [SerializeField]
            private string sentences;
            public AudioClip clip;
            #endregion
            public void StartMonolog()
            {
                if (trigger && !Subtitles.Audi.isPlaying&&!wasPlayed)
                {
                    Subtitles.Txt.text =sentences;
                    Subtitles.Audi.clip = clip;
                    Subtitles.Audi.Play();

			trigger = false;
			wasPlayed = true;


                }
	
				
            }

        }

#endregion



