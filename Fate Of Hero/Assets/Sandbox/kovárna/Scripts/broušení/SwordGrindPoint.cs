using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGrindPoint : MonoBehaviour {
    private float currentSharpened, finalSharpened;

    private GameObject Wheel;







	void Start () {
		
	}
	
	void Update () {
		
	}
    private void OnCollisionStay(Collision collision)
    {
        print("Collision");
    }
}
