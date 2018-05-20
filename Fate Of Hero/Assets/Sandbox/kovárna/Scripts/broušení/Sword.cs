using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    private KeyCode forward = KeyCode.UpArrow;
    private KeyCode backward = KeyCode.DownArrow;
    private KeyCode left = KeyCode.LeftArrow;
    private KeyCode right = KeyCode.RightArrow;
    private float RotateAngelMax = 285;

    void Start () {
		
	}
	
	void Update () {
       if(Input.GetKey(forward)&& transform.eulerAngles.x>RotateAngelMax)
            transform.Rotate(new Vector3(0, -3, 0));
       if(Input.GetKey(backward)&&transform.eulerAngles.x<359)

            transform.Rotate(new Vector3(0, 3, 0));
        print(transform.eulerAngles.x);




    }


 
}
