using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HItPosition : MonoBehaviour
{

    private float MoveSpeed, StartPosition, EndPosition, YPositionDown, YPositionUP, RustSize, RustYSpawn;

    private GameObject Rust;
    private HitPower PowerHit;
    public bool isChoosingPosition;
    private GameObject clons;
    public static bool isTouching;
    private int YRandomPos;
    [SerializeField]
    private RectTransform down, up, max, min;
    [SerializeField]
    private GameObject[] rustSizes;
  
    



    void Start()
    {
        PowerHit = FindObjectOfType<HitPower>();
        StartPosition = min.position.x;
        EndPosition = max.position.x;
        YPositionDown = down.position.y;
        YPositionUP = up.position.y;
        MoveSpeed = 5;
        transform.position = new Vector3(StartPosition, YPositionDown);
        isChoosingPosition = true;
        for (int i = 0; i < 4; i++)
            SetRust();
    }


    void Update()
    {



        if (isChoosingPosition && Blade.HitValue > 0&& clons != null)
            transform.position = new Vector3(transform.position.x + MoveSpeed, transform.position.y);
        if (transform.position.x < EndPosition || transform.position.x > StartPosition)
        {


            MoveSpeed *= -1;
        }

        if (transform.position.x <= EndPosition)
        {


            transform.position = new Vector3(transform.position.x, YPositionUP);
        }
        else if (transform.position.x >= StartPosition)
        {
            transform.position = new Vector3(transform.position.x, YPositionDown);

        }

        if (Input.GetKeyDown(KeyCode.A) && Blade.HitValue > 0 && isChoosingPosition)
        {


            isChoosingPosition = false;
            PowerHit.isChoosingPower = true;
        }

    }


    private void SetRust()
    {


        YRandomPos = Random.Range(1, 3);

        if (YRandomPos == 2)
            RustYSpawn = YPositionDown;

        if (YRandomPos == 1)
            RustYSpawn = YPositionUP;
        switch (Random.Range(1, 4))
        {


            case 1:
                Rust = rustSizes[0];
                break;
            case 2:
                Rust = rustSizes[1];

                break;
            case 3:
                Rust = rustSizes[2];

                break;
        }


        clons = Instantiate(Rust, new Vector3(Random.Range(EndPosition, StartPosition), RustYSpawn), Rust.transform.localRotation);




        clons.transform.SetParent(transform.parent);
        clons.transform.SetSiblingIndex(1);


    }
 
}
