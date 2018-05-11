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
    public static bool isTouching;





    void Start () {
        PowerHit = FindObjectOfType<HitPower>();
        StartPosition = 370;
        EndPosition = -749;
        YPositionDown = -72;
        YPositionUP = 34;
        MoveSpeed = 10;
        transform.localPosition = new Vector3(StartPosition,YPositionDown);
        isChoosingPosition = true;
        for(int i = 0;i<3;i++)
        SetRust();
      

    }


    void Update () {
      


     

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

        clons.GetComponent<RectTransform>().sizeDelta = new Vector2(Random.Range(10, 50) ,Random.Range(10, 50));
       Destroy(clons.GetComponent<CircleCollider2D>());
       clons.AddComponent<CircleCollider2D>();
        clons.transform.SetParent(transform.parent);
        clons.transform.SetSiblingIndex(1);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouching = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;

    }
}
