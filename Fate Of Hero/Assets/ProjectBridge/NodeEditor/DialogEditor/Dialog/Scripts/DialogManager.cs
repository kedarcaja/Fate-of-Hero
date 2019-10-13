using DialogEditor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

 

    public DialogGraph graph;
    private bool isStopped = true, isPaused = false;


    private void Awake()
    {
        if (graph)
        {
            graph.InitDialog();
        }
      
    }
    private void Update()
    {
        if (graph != null && IsPlaying())
        {

            graph.lifeCycle.Tick();

            if (!AudioPlayer.isPlaying && AudioIsReady())
            {
                AudioPlayer.Play();
            }

        }     
    }
    public void  Skip()
    {
        if (IsPlaying())
        {
            graph.Skip();
        }
    }
    public void Play()
    {
        if (!IsPlaying())
        {
            graph.Play();
            isStopped = false;
            isPaused = false;
        }
    }

    public void Pause()
    {
        if (IsPlaying())
        {
            graph.Pause();
            AudioPlayer.Pause();
            isPaused = true;
        }
    }

    public void Stop()
    {
        if (IsPlaying())
        {
            graph.Stop();
            AudioPlayer.Stop();
            isStopped = true;
        }

    }

    public void ChangeGraph(DialogGraph g)
    {
        if (!IsPlaying())
        {
            Stop();
            g.InitDialog();
            graph = g;
        }
        
    }
    private bool AudioIsReady()
    {
        return AudioPlayer.clip != null && AudioPlayer.clip.loadState == AudioDataLoadState.Loaded && AudioPlayer.clip;
    }

    public bool IsPlaying()
    {
        return !isStopped && !isPaused && graph.IsPlaying();
    }
}
public interface IPlayable
{
    void Play();
    void Pause();
    void Stop();
    bool IsPlaying();

}
