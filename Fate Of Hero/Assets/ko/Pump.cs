using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pump : MonoBehaviour {

    private Slider PumpSlider;

    private bool canBoost = true;
    [SerializeField]
    private RectTransform BellowRectTransform;
    private void Awake()
    {
      
        PumpSlider = GetComponent<Slider>();
      
        PumpSlider.maxValue = 135;
        PumpSlider.value = PumpSlider.maxValue;
        PumpSlider.minValue = 70;



    }

    private void Update()
    {


        BellowRectTransform.sizeDelta = new Vector2(207, PumpSlider.value);
       if(PumpSlider.value== PumpSlider.maxValue&&canBoost)
            SpeedBoost();
          
           

          
        
        if (PumpSlider.value == PumpSlider.minValue)
            canBoost = true;


    }



    private void SpeedBoost()
    {
        canBoost = false;

        TemperatureMeter.speed += 5;
    }
}
