using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "New Dialog", fileName = "Dialog")]
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
            DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Speaker.SpeakerName + ": " + subtitles[subtitlesIndex].Text;
            DialogManager.Instance.AudioPlayer.clip = clip;
            DialogManager.Instance.AudioPlayer.Play();
            IsPlaying = true;
            DialogManager.Instance.StartCoroutine(Timer());

            //PlayerScript.Instance. = false;

        };
        OnEnd += () =>
        {


            DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Text;

            DialogManager.Instance.AudioPlayer.clip = null;
            DialogManager.Instance.AudioPlayer.Stop();
            IsPlaying = false;
         //   Player.Instance.CanMove = true;

        };
        OnSubtitlesChange += () =>
        {
            DialogManager.Instance.StopAllCoroutines();
            subtitlesIndex++;
            DialogManager.Instance.SubtitleArea.text = subtitles[subtitlesIndex].Speaker.SpeakerName + ": " + subtitles[subtitlesIndex].Text;
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




