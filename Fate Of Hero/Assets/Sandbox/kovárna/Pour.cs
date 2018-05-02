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
    private float speedRotation,max, min,speed,currentTime,FullTime;
    private TMPToolTip toolTip;
    private void Start()
    {
        Puller = FindObjectOfType<Slider>();
        max = 272;
        min = 359;
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
        if (CanPour&&transform.eulerAngles.x<=max&&toolTip.CurrentValue<toolTip.Max)
        {
            toolTip.CurrentValue += 1;
            CanPour = false;


        }

        print(CanPour);

        if (SetStart)
        {
            Puller.value = Mathf.Lerp(Puller.value, Puller.minValue, Time.deltaTime*speed);
            returnValue = Mathf.Lerp(transform.eulerAngles.x, min, Time.deltaTime*speed);
            transform.eulerAngles = new Vector3(returnValue, transform.eulerAngles.y, transform.eulerAngles.z);

        }
        print(toolTip.CurrentValue);
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


     
    }


