using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Timers;
public class Manager : MonoBehaviour
{


    #region Variables
    [SerializeField]
    private GameObject[] templates, parts;
    private Color clr = Color.white;
    private int index = 0;
    [SerializeField]
    private float speed,positionDifference;
    [SerializeField]
    private Camera myCamera;
    private bool isDropped = false;
    private Vector3 badPositionLeft,badPositionRight,greatPosition,currentPartPosition;
    private bool partWasSelected = false;
    #endregion
    #region Methods
    void Start()
    {
        clr.a = 0;
        for (int i = 0; i < templates.Length; i++)
        {
            templates[i].GetComponent<SpriteRenderer>().color = clr;
            parts[i].SetActive(false);

        }
        parts[index].SetActive(true);


        InitializeNewPart();



    }


    void Update()
    {
        GetInput();
       
    }



    private void GetInput()
    {



        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(myCamera.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, Mathf.Infinity, 512);



            if (hit.collider != null)
            {
                if (hit.collider.tag == "part")
                {
                    if (myCamera.ScreenToWorldPoint(Input.mousePosition).y > -22)
                        parts[index].transform.position = new Vector3(myCamera.ScreenToWorldPoint(Input.mousePosition).x, myCamera.ScreenToWorldPoint(Input.mousePosition).y);
                    else
                        parts[index].transform.position = new Vector3(myCamera.ScreenToWorldPoint(Input.mousePosition).x, parts[index].transform.position.y);
                    partWasSelected = true;
                }
            }
           
        }



        if (partWasSelected&&Input.GetMouseButtonUp(0))
        {
            isDropped = true;
            parts[index].layer = 0;
        }


        if (isDropped)
        {
            if (parts[index].transform.position.x < -1 && parts[index].transform.position.x > -3)
                LerpToPosition(greatPosition);
            else if (parts[index].transform.position.x < -3)
                LerpToPosition(badPositionLeft);
            else if (parts[index].transform.position.x > -1)
                LerpToPosition(badPositionRight);


        }
        if (IsOnTemplatePosition())
        {
            if (index < templates.Length - 1)
                index++;
            InitializeNewPart();
            partWasSelected = false;
        }
    }
    private void InitializeNewPart()
    {
        isDropped = false;
     
        parts[index].SetActive(true);
        badPositionLeft = new Vector3(templates[index].transform.position.x - positionDifference, templates[index].transform.position.y);
        badPositionRight = new Vector3(templates[index].transform.position.x + positionDifference, templates[index].transform.position.y);
        greatPosition = new Vector3(templates[index].transform.position.x, templates[index].transform.position.y);
    }
    private Vector3 LerpToPosition(Vector3 vc)
    {
        return parts[index].transform.position = Vector3.Lerp(parts[index].transform.position,vc,Time.deltaTime*10);

    }


    private bool IsOnTemplatePosition()
    {
        return parts[index].transform.position == greatPosition || parts[index].transform.position == badPositionLeft || parts[index].transform.position == badPositionRight;
    }
    #endregion
}



