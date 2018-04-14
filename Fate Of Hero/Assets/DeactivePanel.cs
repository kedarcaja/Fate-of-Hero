using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivePanel : MonoBehaviour {
  


    void Update () {
        if (gameObject.GetComponent<DeactivePanel>().isActiveAndEnabled)
        {

            gameObject.SetActive(false);

            gameObject.GetComponent<Image>().color = Color.black;
        }
          
    }
}
