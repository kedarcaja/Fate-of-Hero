using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pour : MonoBehaviour {
    private Slider Puller;
    private bool SetStart = false;
    private bool CanPour;
    private float CurrentValue,returnValue;
    [SerializeField]
    private float speedRotation, max, min, speed;
    private TMPToolTip toolTip;
    private ParticleSystem[] HotMetal;
    private void Start()
    {
        HotMetal = FindObjectsOfType<ParticleSystem>();
        for (int i = 0; i < HotMetal.Length; i++)
            HotMetal[i].Stop();
        Puller = FindObjectOfType<Slider>();
        max = 1f;
        min =84f;
        StartCoroutine(delay());
        toolTip = FindObjectOfType<TMPToolTip>();
    }

    public void TurnVat() {
        if (Puller.value >CurrentValue&& transform.eulerAngles.x > max)
        {
            transform.Rotate(new Vector3(0, speedRotation));

           
        }
        if (Puller.value < CurrentValue)
        {

            Puller.interactable = false;
            SetStart = true;
        }
        CurrentValue = Puller.value;
        
	}
    private void Update()
    {
        if (transform.eulerAngles.x<=max&&toolTip.CurrentValue<toolTip.Max)
        {
            if (CanPour){
                toolTip.CurrentValue += 1;
                CanPour = false;
            }
           
                HotMetal[1].Play();
            StartCoroutine(HotMetalWait());
        }
        else
        {


            for (int i = 0; i < HotMetal.Length; i++)
                HotMetal[i].Stop();
        }

       

        if (SetStart)
        {
            Puller.value = Mathf.Lerp(Puller.value, Puller.minValue, Time.deltaTime*speed);
            returnValue = Mathf.Lerp(transform.eulerAngles.x, min, Time.deltaTime*speed);
            transform.eulerAngles = new Vector3(returnValue, transform.eulerAngles.y, transform.eulerAngles.z);

        }
     
        if (Input.GetMouseButtonDown(0))
        {
            SetStart = false;
            Puller.interactable = true;
        }
        if (Input.GetMouseButtonUp(0))
            SetStart = true;

   
    }


 

      IEnumerator delay()
    {


        while (true)
        {
            yield return new WaitForSeconds(2);
            CanPour = true;
        }
        
    }

    IEnumerator HotMetalWait()
    {


        yield return new WaitForSeconds(0.5f);
        HotMetal[0].Play();
    }
     
    }


