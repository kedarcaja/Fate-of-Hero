using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour {
    
    private int markHealth;
    private bool isGrindable;
	void Start () {
        markHealth = 50;
	}
	
	
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {


        print(gameObject.name + " health: " + markHealth);
        if(markHealth>0)
        markHealth--;

    }


}
