using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashControler: MonoBehaviour
{
    [SerializeField]
    private Image image;
    Color c;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Text KeyText;
    
    private bool KeyActive;
    
    private bool fadingIn, fadingOut;
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    public AudioClip sound;
    [SerializeField]
    private AudioSource Source { get { return GetComponent<AudioSource>(); } }

    void Start () {
        
        if (!image) { Debug.LogError("<color=Red><b>ERROR: </b> The image object was not found </color>"); }
        if (!canvasGroup) { Debug.LogError("<color=Red><b>ERROR: </b> The canvasGroup object was not found </color>"); }
        if (!KeyText) { Debug.LogError("<color=Red><b>ERROR: </b> The KeyText object was not found </color>"); }


        StartCoroutine("FadeIn");
        KeyText.gameObject.SetActive(false);
        gameObject.AddComponent<AudioSource>();
        Source.clip = sound;
        Source.playOnAwake = false;
    }	
	void Update () {
        if (canvasGroup.alpha == 1)
        {
            KeyText.gameObject.SetActive(true);
            KeyActive = true;
        }

        if (KeyActive == true && Input.anyKeyDown)
        {
            PlaySound();
           Invoke("LoadNextLevel", 1);
        }
	}
    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
    void PlaySound()
    {
        Source.PlayOneShot(sound);
    }
    private IEnumerator FadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlfa = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlfa, 0, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0;
            fadingOut = false;
        }

    }
    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlfa = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlfa, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
            fadingIn = false;
        }

    }
}
