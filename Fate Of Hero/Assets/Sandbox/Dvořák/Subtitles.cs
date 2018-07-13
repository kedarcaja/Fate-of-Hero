using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.IMGUI;

public class Subtitles : MonoBehaviour
{
    #region Class Variables
    private static AudioSource audioSource;

    [HideInInspector]
    public static AudioSource audi
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
        txt = FindObjectOfType<Text>();
        audioSource = FindObjectOfType<Text>().GetComponent<AudioSource>();
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
            #endregion
        }
        #region Monolog Methods
        for (int i = 0; i < Monologs.Length; i++)
        {
            Monologs[i].StartMonolog();
            if (Monologs[i].trigger)
            {
                Monologs[i].trigger = false;
            }

        }

        if (!audi.isPlaying)
        {
            audi.clip = null;
            txt.text = "Empty";
            FindObjectOfType<PlayerController>().IsMove = true;

        }
        if (audi.isPlaying)
        {
            FindObjectOfType<PlayerController>().IsMove = false;
        }
    }
    #endregion
  
}





#region Dialog

[Serializable]
        public struct Dialog
        {


   
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
        if (trigger&&!Subtitles.audi.isPlaying)
        {
            sentenceIndex = 0;


            currentSentence = sentences[sentenceIndex];

         
            Subtitles.audi.clip = clip;
            Subtitles.audi.Play();
            Subtitles.Txt.text = "<b>" + currentSentence.speaker + ": " + "</b>" + currentSentence.sentence;
            currentSentence = sentences[sentenceIndex];

        }

    



    }
    public IEnumerator StartTimer()
    {
        while (Subtitles.audi.isPlaying&&Subtitles.audi.clip == clip)
        {
            yield return new WaitForSecondsRealtime(1);
          
                currentSentence.timer--;



            if (currentSentence.timer ==0&&sentenceIndex<sentences.Length) {
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

    [Multiline]
    public string sentence;

    public string speaker;

    [Range(1, 100)]

    public float timer;

}
#endregion
#region Monolog

[Serializable]
        public struct Monolog
        {
            #region Monolog Variables
        
            public string monologName;
            public bool trigger;
 
            [Space]
            [Space]


            [Multiline]
            [SerializeField]
            private string sentences;
            [SerializeField]
            private AudioClip clip;
            #endregion
            public void StartMonolog()
            {
                if (trigger && !Subtitles.audi.isPlaying)
                {
                    Subtitles.Txt.text =sentences;
                    Subtitles.audi.clip = clip;
                    Subtitles.audi.Play();
                    trigger = false;


                }

            }

        }

#endregion



