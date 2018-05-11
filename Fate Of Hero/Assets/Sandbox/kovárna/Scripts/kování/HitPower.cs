using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPower : MonoBehaviour {
 public static bool removeRust;
    private Slider PowerSlider;
    private float PowerSpeed;
    private double PowerValue;
    public bool isChoosingPower = false;
    private HItPosition PositionHit;
    [SerializeField]
    private GameObject Hammer;
    private Animator HammerAnimator;
    [SerializeField]
    private Text PowerText,PowerValueText;
   

    void Start () {
        PowerSlider = GetComponent<Slider>();
        PowerSlider.interactable = false;
        PowerSpeed = 0.01f;
        PositionHit = FindObjectOfType<HItPosition>();
        Hammer.SetActive(false);
        HammerAnimator = Hammer.GetComponent<Animator>();
      
        PowerText.text = "";
       
	}

    void Update()
    {

        PowerValueText.text = Blade.HitValue.ToString();
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

        if (Input.GetKeyDown(KeyCode.S) && Blade.HitValue > 0 && isChoosingPower)

        {
            removeRust = true;
            Hammer.SetActive(true);
            Hammer.transform.localPosition = PositionHit.gameObject.transform.localPosition;

            PowerValue = System.Math.Round(PowerSlider.value, 1);
            if (PowerValue >= 0.4f && PowerValue < 0.6f)
            {

                PowerValue *= 1.5f;
                PowerText.text = "Great";
                PowerText.color = Color.green;
            }
            else if (PowerValue > 0.6f)
            {
                PowerValue /= 10;
                PowerText.text = "To much";
                PowerText.color = Color.red;

            }
            else if (PowerValue < 0.4)
            {
                PowerValue /= 2;
                PowerText.text = "To low";
                PowerText.color = Color.cyan;

            }
            HammerAnimator.SetTrigger("Hit");

            Blade.AllHitPower += PowerValue;
            Blade.HitValue--;
            isChoosingPower = false;
           


        }
      
    }
   
}
