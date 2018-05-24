using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Sword : MonoBehaviour {


    [SerializeField]
    private float moveSpeed,rotateSpeed;
    private GameObject sword;
    private int turns = 0;
    private float MaxPositionX,MinPositionX;
    private void Start()
    {
        sword = GameObject.Find("Sword");
        MaxPositionX = -3.15698f;

        MinPositionX = -3.543f;
    }
    void Update ()
    {

        if (Input.GetKey(KeyCode.A)&&transform.position.z<=MaxPositionX) 
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);


        if (Input.GetKey(KeyCode.D)&&transform.position.z>=MinPositionX)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);



        if (Input.GetKeyDown(KeyCode.S)&& turns>-1)
        {
            sword.transform.Rotate(new Vector3(0, 0, rotateSpeed));

            turns--;

        }





        if (Input.GetKeyDown(KeyCode.W)&&turns < 1)
        {
            sword.transform.Rotate(new Vector3(0, 0, -rotateSpeed));
            turns++;
        }


        if (turns == 0)
            Wheel.swordIsInContact = false;
        else
            Wheel.swordIsInContact = true;

    }
 


}
