using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour {

    #region Variables
    public Image image;
    Color c;
    public CanvasGroup canvasGroup;
    public Text Klavesa;
    private bool KeyActive;
    private bool fadingIn, fadingOut;
    public float fadeTime;
    public AudioClip sound;
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

#pragma warning disable CS0618 // Typ nebo člen je zastaralý.
        Application.LoadLevel(Application.loadedLevel + 1);
#pragma warning restore CS0618 // Typ nebo člen je zastaralý.

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
