using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pour : MonoBehaviour {
    private Slider Puller;
    private bool SetStart = false;
    private float CurrentValue,returnValue;
    [SerializeField]
    private float speed,max, min;
    private void Start()
    {
        Puller = FindObjectOfType<Slider>();
        max = 280;
        min = 359;
    }

    public void TurnVat() {
        if (Puller.value >CurrentValue&& transform.eulerAngles.x > max)
        {
            transform.Rotate(new Vector3(0, speed));
           

        }
     
        CurrentValue = Puller.value;
        
	}
    private void Update()
    {



        if (SetStart)
        {
            Puller.value = Mathf.Lerp(Puller.value, Puller.minValue, Time.deltaTime);
            returnValue = Mathf.Lerp(transform.eulerAngles.x, min, Time.deltaTime);
            transform.eulerAngles = new Vector3(returnValue, transform.eulerAngles.y, transform.eulerAngles.z);

        }

        if (Input.GetMouseButtonDown(0))
            SetStart = false;
        if (Input.GetMouseButtonUp(0))
            SetStart = true;
    }
    
}
