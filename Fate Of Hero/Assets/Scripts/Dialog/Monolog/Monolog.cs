using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "New Monolog", fileName = "Monolog")]
public class Monolog : ScriptableObject,IInterpretable
{
    [SerializeField]
    private AudioClip clip;
    [TextAreaAttribute(0, 50)]
    [SerializeField]
    private string subtitles;

    

    public AudioClip Clip { get { return clip; } }
    public string Subtitle { get { return subtitles; } }
    public float Time { get; set; }

    public DialogHandler OnStart;
    public DialogHandler OnEnd;



    // Must be called before using monolog
    public void Init()
    {
        OnStart += () =>
        {
            Time = Clip.length;
            DialogManager.Instance.SubtitleArea.text = Subtitle;
            DialogManager.Instance.AudioPlayer.clip = Clip;
            DialogManager.Instance.AudioPlayer.Play();
            PlayerScript.Instance.CanMove = false;

          DialogManager.Instance.StartCoroutine(Timer());

        };
        OnEnd += () =>
        {
            DialogManager.Instance.SubtitleArea.text = string.Empty;
            DialogManager.Instance.AudioPlayer.clip = null;
            DialogManager.Instance.AudioPlayer.Stop();
            PlayerScript.Instance.CanMove = true;



        };

    }



    IEnumerator Timer()
    {

        yield return new WaitWhile(() => DialogManager.Instance.AudioPlayer.isPlaying && DialogManager.Instance.AudioPlayer.clip == clip);


        OnEnd();

    }
}
public delegate void DialogHandler();

public interface IInterpretable
{
    AudioClip Clip { get; }
    void Init();
}