using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public CanvasGroup canvasGroupI, canvasGroupII;
    private bool fadingIn, fadingOut;
    public float fadeTime;
    public bool instantClose = true;

    public void Click()
    {
      
        if (canvasGroupI.alpha > 0)
        {
            StartCoroutine("FadeOutI");
            canvasGroupI.blocksRaycasts = false;

            StartCoroutine("FadeInII");
            canvasGroupII.blocksRaycasts = true;
        }
        else
        {
            StartCoroutine("FadeInI");
            canvasGroupII.blocksRaycasts = true;

            StartCoroutine("FadeOutII");
            canvasGroupI.blocksRaycasts = false;
        }
    }

    private IEnumerator FadeOutI()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlfa = canvasGroupI.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroupI.alpha = Mathf.Lerp(startAlfa, 0, progress);
                progress += rate * Time.deltaTime;
                if (instantClose)
                {
                    break;
                }
                yield return null;
            }
            canvasGroupI.alpha = 0;
            instantClose = false;
            fadingOut = false;
        }

    }
    private IEnumerator FadeInI()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlfa = canvasGroupI.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroupI.alpha = Mathf.Lerp(startAlfa, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroupI.alpha = 1;
            fadingIn = false;
        }

    }

    private IEnumerator FadeOutII()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlfa = canvasGroupII.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroupII.alpha = Mathf.Lerp(startAlfa, 0, progress);
                progress += rate * Time.deltaTime;
                if (instantClose)
                {
                    break;
                }
                yield return null;
            }
            canvasGroupII.alpha = 0;
            instantClose = false;
            fadingOut = false;
        }

    }
    private IEnumerator FadeInII()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlfa = canvasGroupII.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroupII.alpha = Mathf.Lerp(startAlfa, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroupII.alpha = 1;
            fadingIn = false;
        }

    }
}
