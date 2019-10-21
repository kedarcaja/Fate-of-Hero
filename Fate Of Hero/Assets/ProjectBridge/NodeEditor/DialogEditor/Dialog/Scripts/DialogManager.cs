using DialogEditor;
using FourGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Extensions;
using Unity.UIExtension;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour, IPlayable
{

    [SerializeField]
    private TextMeshProUGUI subtitleArea;
    public TextMeshProUGUI SubtitleArea { get => subtitleArea; }
    public AudioSource AudioPlayer { get { return GetComponent<AudioSource>(); } }
    public static DialogManager Instance { get { return FindObjectOfType<DialogManager>(); } }
    private DialogScript sender;

    public DialogGraph graph;
    private bool isStopped = true, isPaused = false;


    private void Awake()
    {
        if (graph)
        {
            graph.wasPlayed = false;
            graph.InitDialog();
        }

    }
    private void Update()
    {
        if (graph != null)
        {
            if (IsPlaying())
            {
                graph.lifeCycle.Tick();
                if (!AudioPlayer.isPlaying && AudioIsReady())
                {
                    AudioPlayer.Play();
                }
            }

        }
    }

    public void Play()
    {
        if (!IsPlaying())
        {
            graph.InitDialog();
            subtitleArea.GetComponent<CanvasGroup>().Active(true);
            AudioPlayer.Play();
            graph.lifeCycle.Play();
            isStopped = false;
            isPaused = false;
        }
    }



    public void Stop()
    {
        AudioPlayer.Stop();
        graph = null;
        AudioPlayer.clip = null;
        isStopped = true;
        subtitleArea.text = "";
        subtitleArea.GetComponent<CanvasGroup>().Deactive(true);
        if (sender != null && sender.Graph.destroyOnEnd)
        {
            Destroy(sender.gameObject);
        }
        PlayerScript.Instance.EnableMove();

    }

    public void ChangeGraph(DialogScript g)
    {
        if (!IsPlaying())
        {
            Stop();
            if (graph)
                graph.wasPlayed = false;
            g.Graph.InitDialog();
            graph = g.Graph;
            sender = g;
        }

    }
    private bool AudioIsReady()
    {
        return AudioPlayer.clip != null && AudioPlayer.clip.loadState == AudioDataLoadState.Loaded && AudioPlayer.clip;
    }


    public bool IsPlaying()
    {

        return graph ? graph.lifeCycle.IsPlaying() : false;
    }

    public void Pause()
    {

    }
}
public interface IPlayable
{
    void Play();
    void Pause();
    void Stop();
    bool IsPlaying();

}
