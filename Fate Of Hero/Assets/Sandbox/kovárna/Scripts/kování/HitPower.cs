using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPower : MonoBehaviour {

    private Slider PowerSlider;
    private float PowerValue,PowerSpeed;
    public bool isChoosingPower = false;
    private HItPosition PositionHit;
    [SerializeField]
    private GameObject Hammer;
    private Animator HammerAnimator;
        void Start () {
        PowerSlider = GetComponent<Slider>();
        PowerSlider.interactable = false;
        PowerSpeed = 0.01f;
        PositionHit = FindObjectOfType<HItPosition>();
        Hammer.SetActive(false);
        HammerAnimator = Hammer.GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.S) && Blade.HitValue > 0&&isChoosingPower)

        {
            Hammer.SetActive(true);
            Hammer.transform.localPosition = PositionHit.gameObject.transform.localPosition;
            HammerAnimator.SetTrigger("Hit");
            PowerValue = PowerSlider.value;
            Blade.AllHitPower += PowerValue;
            Blade.HitValue--;

            if (!HammerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                isChoosingPower = false;
                PositionHit.isChoosingPosition = true;
           

               
            }

        }
    }
}
