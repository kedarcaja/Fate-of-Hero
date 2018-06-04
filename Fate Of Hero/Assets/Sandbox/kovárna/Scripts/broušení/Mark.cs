using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mark : MonoBehaviour {
    
    private float markHealth,startHealth;
    [SerializeField]
    private Text markText;
	void Start () {
        startHealth = 100;
        markHealth = startHealth;

     
    }


    void Update () {
        markText.text =gameObject.name+" "+ Percentile() + "%";
	}
    private void OnTriggerEnter(Collider other)
    {
      

     
      if(markHealth>0&&Wheel.isRotating)
        markHealth--;

    }

    private float Percentile()
    {


        return (markHealth/startHealth) * 100;
    }
}
