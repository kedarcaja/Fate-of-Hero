using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HItPosition : MonoBehaviour {

    private float MoveSpeed,StartPosition,EndPosition,YPositionDown,YPositionUP;
    [SerializeField]
    private GameObject Rust;
    private HitPower PowerHit;
    public bool isChoosingPosition;
    private GameObject clons;



  

    void Start () {
        PowerHit = FindObjectOfType<HitPower>();
        StartPosition = 370;
        EndPosition = -749;
        YPositionDown = -72;
        YPositionUP = 34;
        MoveSpeed = 10;
        transform.localPosition = new Vector3(StartPosition,YPositionDown);
        isChoosingPosition = true;

        SetRust();
      

    }


    void Update () {
        if (clons == null)
        {
            SetRust();
            
           
           

        }
       
         


        if (isChoosingPosition&& Blade.HitValue > 0)
        transform.localPosition = new Vector3(transform.localPosition.x + MoveSpeed, transform.localPosition.y);
		if(transform.localPosition.x < EndPosition || transform.localPosition.x > StartPosition)
        {


            MoveSpeed *= -1;
        }

        if (transform.localPosition.x <= EndPosition)
        {


            transform.localPosition = new Vector3(transform.localPosition.x, YPositionUP);
        }
        else if(transform.localPosition.x>=StartPosition)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, YPositionDown);

        }

        if (Input.GetKeyDown(KeyCode.A) && Blade.HitValue > 0&&isChoosingPosition)
        {


            isChoosingPosition = false;
            PowerHit.isChoosingPower = true;
        }


    }

   
    private void SetRust()
    {




       
        clons =  Instantiate(Rust,new Vector3(Random.Range(100,600),transform.position.y) ,Rust.transform.localRotation);
        
        clons.transform.SetParent(transform.parent);
        clons.transform.SetSiblingIndex(1);


    }

}
