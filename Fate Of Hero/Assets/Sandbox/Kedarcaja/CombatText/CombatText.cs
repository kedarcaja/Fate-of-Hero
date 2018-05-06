using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private float fadeTime;
    public AnimationClip critAnim;
    private bool crit;
	void Update () {
        
         float translation = speed * Time.deltaTime;
        transform.Translate(direction * translation);
	}
    public void Initialize(float speed, Vector3 didection,float fadeTime,bool crit)
    {
        this.speed = speed;
        this.fadeTime = fadeTime;
        this.direction = didection;
        this.crit = crit;
        if (crit)
        {
            GetComponent<Animator>().SetTrigger("Critical");
            crit = false;
            StartCoroutine(Critical());
        }
        else
        {
            
            StartCoroutine(Fadeout());
        }  
    }
    private IEnumerator Critical()
    {
        yield return new WaitForSeconds(critAnim.length);
        StartCoroutine(Fadeout());
    }
    private IEnumerator Fadeout()
    {
        float startAlpha = GetComponent<Text>().color.a;

        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0)
        {
            Color tmpColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));

            progress += rate * Time.deltaTime;

            yield return null;
        }
        Destroy(gameObject);
    }
}
