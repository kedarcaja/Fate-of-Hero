using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashControler: MonoBehaviour {

    #region Variables
    [SerializeField]
    private Image image;
    Color c;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Text Klavesa;
    [SerializeField]
    private bool KeyActive;
    [SerializeField]
    private bool fadingIn, fadingOut;
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    public AudioClip sound;
    [SerializeField]
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    #endregion

    #region Unity Metod

    void Start () {
        StartCoroutine("FadeIn");
        Klavesa.gameObject.SetActive(false);
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
    }	
	void Update () {
        if (canvasGroup.alpha == 1)
        {
            Klavesa.gameObject.SetActive(true);
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
#pragma warning disable CS0618 
        Application.LoadLevel(Application.loadedLevel + 1);
#pragma warning restore CS0618 
    }
    void PlaySound()
    {
        source.PlayOneShot(sound);
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
    #endregion
}
