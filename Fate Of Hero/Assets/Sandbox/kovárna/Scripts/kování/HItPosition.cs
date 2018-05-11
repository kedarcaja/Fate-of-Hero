using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HItPosition : MonoBehaviour {

    private float MoveSpeed,StartPosition,EndPosition,YPositionDown,YPositionUP,RustSize,RustYSpawn;
    [SerializeField]
    private GameObject Rust;
    private HitPower PowerHit;
    public bool isChoosingPosition;
    private GameObject clons;
    public static bool isTouching;
    private int YRandomPos;





    void Start () {
        PowerHit = FindObjectOfType<HitPower>();
        StartPosition = 370;
        EndPosition = -749;
        YPositionDown = -115;
        YPositionUP = 93;
        MoveSpeed = 10;
        transform.localPosition = new Vector3(StartPosition,YPositionDown);
        isChoosingPosition = true;
        for(int i = 0;i<10;i++)
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
        print(transform.position.y);

    }

   
    private void SetRust()
    {


        YRandomPos = Random.Range(1, 3);

        if (YRandomPos == 2)
            RustYSpawn = transform.position.y;

        if (YRandomPos == 1)
            RustYSpawn = 313.5641f;



        clons =  Instantiate(Rust,new Vector3(Random.Range(100,600),RustYSpawn) ,Rust.transform.localRotation);
        RustSize = Random.Range(5, 80);
        clons.GetComponent<RectTransform>().sizeDelta = new Vector2(RustSize ,5);
       Destroy(clons.GetComponent<BoxCollider2D>());
        clons.AddComponent<BoxCollider2D>().size = new Vector2((RustSize/4),5);
        clons.GetComponent<BoxCollider2D>().offset.Normalize();
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
