using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPower : MonoBehaviour {

    private Slider PowerSlider;
    private float PowerValue,PowerSpeed;
	void Start () {
        PowerSlider = GetComponent<Slider>();
        PowerSlider.interactable = false;
        PowerSpeed = 0.01f;
	}

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetMouseButtonDown(0))
        { PowerValue = PowerSlider.value;
            PowerSpeed = 0;
        }
    }
}
