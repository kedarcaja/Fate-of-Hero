using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Kladivo : MonoBehaviour {
    private float start, End, YDown, YUp, Movespeed,powerSpeed,PowerValue;
    private bool isChoosingPosition = true;
    private bool isChoosingPower = false;
    [SerializeField]
    private Slider Power;

    private void Start()
    {
        start = 457;
        End = -650;
        YUp = -9;
            YDown = -160;
        transform.localPosition = new Vector3(start,YDown);
        Movespeed = 10;
        powerSpeed = 0.01f;
        Power.interactable = false;
       


    }
    private void Update()
    {


        if(isChoosingPosition)
        transform.localPosition = new Vector3(transform.localPosition.x - Movespeed, transform.localPosition.y);
        if (transform.localPosition.x <= End || transform.localPosition.x >= start)
        {

            Movespeed *= -1;

        }
        if (transform.localPosition.x >= start && transform.localPosition.y >= YUp)
            transform.localPosition = new Vector3(transform.localPosition.x, YDown);
        if (transform.localPosition.x <= End && transform.localPosition.y <= YDown)
            transform.localPosition = new Vector3(transform.localPosition.x, YUp);
        if (Input.GetMouseButtonDown(0)&&isChoosingPosition)
        {


            isChoosingPosition = false;
            StartCoroutine(Delay());
        }
        if (isChoosingPower)
        {
            Power.value += powerSpeed;
            if (Power.value == Power.maxValue || Power.value == Power.minValue)
            {
                powerSpeed *= -1;
                if (powerSpeed > 0)
                    powerSpeed += 0.001f;
                if (powerSpeed < 0)
                    powerSpeed += -0.001f;


            }
            if (Input.GetMouseButtonDown(0))
            {
                isChoosingPower = false;
                PowerValue = Power.value;
            }
        }

      
      

    }
    IEnumerator Delay()
    {



        yield return new WaitForSeconds(0.001f);
        isChoosingPower = true;
    }
}
