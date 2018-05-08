using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

public static float AllHitPower;
    public static int HitValue;
	void Start () {
        HitValue = 10;
		
	}
	

	void Update () {
     print("Current Power: "+AllHitPower);
  

    }
}
