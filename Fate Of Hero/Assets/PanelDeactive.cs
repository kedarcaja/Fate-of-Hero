using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelDeactive : MonoBehaviour {

    void Update()
    {
        if (gameObject.GetComponent<PanelDeactive>().isActiveAndEnabled)
        {

            gameObject.SetActive(false);

            gameObject.GetComponent<Image>().color = Color.black;
            gameObject.GetComponent<PanelDeactive>().enabled = false;

        }


    }


    public void ResetPanel()
    {
        gameObject.SetActive(true);




    }
}
