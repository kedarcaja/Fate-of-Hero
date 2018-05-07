using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HItPosition : MonoBehaviour {

    private float MoveSpeed,StartPosition,EndPosition,YPositionDown,YPositionUP;
    [SerializeField]
    private GameObject PowerSlider;
	void Start () {
        StartPosition = 370;
        EndPosition = -749;
        YPositionDown = -72;
        YPositionUP = 34;
        MoveSpeed = 10;
        transform.localPosition = new Vector3(StartPosition,YPositionDown);

	}
	

	void Update () {


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

        if (Input.GetMouseButtonDown(0))
        {


            MoveSpeed = 0;
            PowerSlider.SetActive(true);
        }
    }
}
