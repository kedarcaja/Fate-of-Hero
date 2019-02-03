using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/New Dialog")]
public class Dialog : ScriptableObject, IInterpretable
{
    [SerializeField]
    private AudioClip clip;

    public AudioClip Clip { get { return clip; } }
    [SerializeField]
    private List<Dialogoure> subtitles;

    public List<Dialogoure> Subtitles { get { return subtitles; } }
    private int subtitlesIndex = 0;
    private int maxIndex;
    public DialogHandler OnStart;
    public DialogHandler OnEnd;
    public DialogHandler OnSubtitlesChange;
    public bool IsPlaying { get; set; }
    public bool WasPlayed { get; set; }
  



    // Must be called before using Dialog
    public void Init()
    {
        maxIndex = subtitles.Count - 1;

        foreach (Dialogoure item in subtitles)
        {
            item.Time = item.StartTimer;

        }

        OnStart += () =>
        {
            subtitlesIndex = 0;
            if (subtitles[subtitlesIndex].Speaker != null)
            {
                DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Speaker.SpeakerName + subtitles[subtitlesIndex].Text;
            }
            else
            {
                DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Text;
            }
           
            DialogManager.Instance.AudioPlayer.clip = clip;
            DialogManager.Instance.AudioPlayer.Play();
            IsPlaying = true;
            DialogManager.Instance.StartCoroutine(Timer());
            WasPlayed = true;
            if (PlayerScript.Instance != null)
            { PlayerScript.Instance.CanMove = false; 
            PlayerScript.Instance.MyAnimator.SetFloat("Speed", 0);
            }
            DialogManager.Instance.SubtitleArea.gameObject.SetActive(true);

            if (PlayerScript.Instance != null)
            {
                PlayerScript.Instance.MyAgent.isStopped = true;
                PlayerScript.Instance.MyAgent.destination = PlayerScript.Instance.transform.position;//player turning
            }

        };
        OnEnd += () =>
        {
            DialogManager.Instance.StopAllCoroutines();

            DialogManager.Instance.SubtitleArea.text = "";

            DialogManager.Instance.AudioPlayer.clip = null;
            DialogManager.Instance.AudioPlayer.Stop();
            IsPlaying = false;
            if(PlayerScript.Instance != null)
            { PlayerScript.Instance.CanMove = true;
               PlayerScript.Instance.MyAgent.isStopped = false;
            }
            
            

            DialogManager.Instance.SubtitleArea.gameObject.SetActive(false);




        };
        OnSubtitlesChange += () =>
        {
            DialogManager.Instance.StopAllCoroutines();
            subtitlesIndex++;
            if (subtitles[subtitlesIndex].Speaker != null)
            {
                DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Speaker.SpeakerName + subtitles[subtitlesIndex].Text;
            }
            else
            {
                DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Text;
            }           
            DialogManager.Instance.StartCoroutine(Timer());


        };

    }

    public IEnumerator Timer()
    {

        yield return new WaitForSeconds(Subtitles[subtitlesIndex].StartTimer);

        if (subtitlesIndex < maxIndex)
        {
            OnSubtitlesChange();
        }
        else
        {
            OnEnd();
        }
    }




}




