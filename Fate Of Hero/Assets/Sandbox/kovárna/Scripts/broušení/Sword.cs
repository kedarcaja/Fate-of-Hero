using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Sword : MonoBehaviour {


    [SerializeField]
    private float moveSpeed,rotateSpeed;
    private GameObject sword;
    private void Start()
    {
        sword = GameObject.Find("Sword");
       
    }
    void Update ()
    {

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);


        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);



        if (Input.GetKeyDown(KeyCode.S))
            sword.transform.Rotate(new Vector3(0,0, rotateSpeed));



        if (Input.GetKeyDown(KeyCode.W))
            sword.transform.Rotate(new Vector3(0, 0, -rotateSpeed));
       

    }



}
