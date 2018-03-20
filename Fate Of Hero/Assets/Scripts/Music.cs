using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Music : MonoBehaviour {
    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;
    public AudioClip[] stings;
    public AudioSource stingSource;
    public float bpm = 128;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    // Use this for initialization
    void Start()
    {
        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;

    }

    public void music()
    {
        outOfCombat.TransitionTo(m_TransitionOut);

    }
    public void Voice() {
        inCombat.TransitionTo(m_TransitionIn);
        PlaySting();
    }

    void PlaySting()
    {
        stingSource.Play();
    }
}
