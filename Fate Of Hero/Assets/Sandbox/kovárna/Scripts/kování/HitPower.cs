using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPower : MonoBehaviour {

    private Slider PowerSlider;
    private float PowerValue,PowerSpeed;
    public bool isChoosingPower = false;
    private HItPosition PositionHit;
        void Start () {
        PowerSlider = GetComponent<Slider>();
        PowerSlider.interactable = false;
        PowerSpeed = 0.01f;
        PositionHit = FindObjectOfType<HItPosition>();
	}

    // Update is called once per frame
    void Update()
    {
        if (isChoosingPower)
            PowerSlider.value += PowerSpeed;
            if (PowerSlider.value == PowerSlider.minValue)
            {
                PowerSpeed -= 0.001f;
                PowerSpeed *= -1;

            }
            if (PowerSlider.value == PowerSlider.maxValue)
            {
                PowerSpeed += 0.001f;

                PowerSpeed *= -1;

            }

        if (Input.GetKeyDown(KeyCode.S)&&Blade.HitValue>0)

        {
            PowerValue = PowerSlider.value;
            isChoosingPower = false;
            PositionHit.isChoosingPosition = true;
           
            Blade.AllHitPower += PowerValue;
            Blade.HitValue--;

        }
    }
}
