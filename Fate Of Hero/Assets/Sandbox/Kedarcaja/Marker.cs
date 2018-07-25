using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Marker : MonoBehaviour {


    
    
    [SerializeField]
    private Image image;
    [SerializeField]
    private CanvasGroup canvasGroup;


    public bool StopCry()
    {
        return true;
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            canvasGroup.alpha = 1;
            StartCoroutine(Timer());

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvasGroup.alpha = 0;
        }
    }

    IEnumerator Timer()
    {
        
        yield return new WaitForSeconds(2);
        canvasGroup.alpha = 0;
    }
}
