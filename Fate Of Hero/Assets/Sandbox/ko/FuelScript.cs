using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelScript : MonoBehaviour {
    private Button FuelButton;
    private Animator FuelAnimator;
    public  bool isInInventory = false;
    private void Awake()
    {
        FuelButton = GetComponent<Button>();
        FuelAnimator = GetComponent<Animator>();
        TemperatureMeter.HasStarted = false;
    }
    void Update () {
        if (isInInventory)
        {



            FuelButton.onClick.AddListener(() => ActiveAnimation());




        }
        else
            FuelButton.interactable = false;
    }

    private void ActiveAnimation()
    {

        TemperatureMeter.HasStarted = true;
      FuelAnimator.enabled = true;

    }
}
