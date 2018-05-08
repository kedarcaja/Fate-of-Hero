using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HItPosition : MonoBehaviour {

    private float MoveSpeed,StartPosition,EndPosition,YPositionDown,YPositionUP;
   
    private HitPower PowerHit;
   public bool isChoosingPosition;
	void Start () {
        PowerHit = FindObjectOfType<HitPower>();
        StartPosition = 370;
        EndPosition = -749;
        YPositionDown = -72;
        YPositionUP = 34;
        MoveSpeed = 10;
        transform.localPosition = new Vector3(StartPosition,YPositionDown);
        isChoosingPosition = true;
	}
	

	void Update () {

        if(isChoosingPosition&& Blade.HitValue > 0)
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
}
