using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Sword : MonoBehaviour {


   
    void Start () {
		
	}
	
	void Update () {
       if(Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(new Vector3(0, 0, -3));
        if (Input.GetKey(KeyCode.DownArrow))

            transform.Rotate(new Vector3(0, 0, 3));
       




    }


 
}
