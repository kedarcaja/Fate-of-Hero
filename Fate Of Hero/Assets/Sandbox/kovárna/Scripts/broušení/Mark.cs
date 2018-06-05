using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mark : MonoBehaviour {
    
    private float markHealth,startHealth;
    [SerializeField]
    private Text markText;
    [SerializeField]
    private Image mark;
	void Start () {
        startHealth = 150;
        markHealth = startHealth;

        markText.color = Color.white;
        mark.color = Color.red;
    }


    void Update () {
        markText.text =Percentile() + "%";
       // setMarkValueColor();
	}
    private void OnTriggerEnter(Collider other)
    {



        if (markHealth > 0 && Wheel.isRotating&&Wheel.Speed<-5)
        {

            markHealth--;
            mark.color = Color.Lerp(mark.color, Color.green, Time.deltaTime);


        }
        else if (markHealth > 0 && Wheel.isRotating && Wheel.Speed > -5)
        {

            markHealth-=0.5f;
            mark.color = Color.Lerp(mark.color, Color.green, Time.deltaTime);

        }


    }

    private float Percentile()
    {
        return Mathf.Round((markHealth / startHealth) * 100);
    }


    private void setMarkValueColor()
    {

        if (Percentile() == 100) {
            markText.color = Color.red;

        }
        else if (Percentile() == 50)
        {

            markText.color = Color.yellow;
        }
        else if (Percentile() == 0)
        {

            markText.color = Color.green;
        }
    }
}
