using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pour : MonoBehaviour {
    private Slider Puller;
    private bool SetStart = false;
    private float CurrentValue,returnValue;
    [SerializeField]
    private float speedRotation,max, min,speed;
    private void Start()
    {
        Puller = FindObjectOfType<Slider>();
        max = 280;
        min = 359;
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
    
}
