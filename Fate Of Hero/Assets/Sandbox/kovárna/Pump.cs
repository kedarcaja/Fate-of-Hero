using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pump : MonoBehaviour {

    private Slider PumpSlider;

    private bool canBoost = true;
    [SerializeField]
    private RectTransform BellowRectTransform;
    [SerializeField]
   
    private void Awake()
    {
      
        PumpSlider = GetComponent<Slider>();
      
        PumpSlider.maxValue = 308f;
        PumpSlider.value = PumpSlider.maxValue;
        PumpSlider.minValue = 180;
            gameObject.SetActive(false);



    }

    private void Update()
    {


        BellowRectTransform.sizeDelta = new Vector2(450, PumpSlider.value);

        if (TemperatureMeter.HasStarted)
        {
           
            if (PumpSlider.value == PumpSlider.maxValue && canBoost)
            {


                SpeedBoost();


            }
        }
          
           

          
        
        if (PumpSlider.value == PumpSlider.minValue)
            canBoost = true;


    }



    private void SpeedBoost()
    {
        canBoost = false;
        if (TemperatureMeter.speed<30)
        TemperatureMeter.speed += 5;
    }
}
