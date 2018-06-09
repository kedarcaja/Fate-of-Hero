using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Timers;
public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] templates, parts;
    private Color clr = Color.white;
    private int index = 0;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Camera myCamera;
    private bool isDropped = false;

    void Start()
    {
        clr.a = 0;
        for (int i = 0; i < templates.Length; i++)
        {
            templates[i].GetComponent<SpriteRenderer>().color = clr;
            parts[i].SetActive(false);

        }
        parts[index].SetActive(true);
    }


    void Update()
    {
        GetInput();
        print(parts[index].transform.position.x);
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

                }
            }
        }
        if (Input.GetMouseButtonUp(0) && templates[index].transform.position != parts[index].transform.position)
        {
            isDropped = true;
        }





        if (isDropped)
        {
            if (parts[index].transform.position.x < -1 && parts[index].transform.position.x > -3)
                parts[index].transform.position = Vector3.Lerp(parts[index].transform.position, new Vector3(templates[index].transform.position.x, templates[index].transform.position.y), Time.deltaTime * 10f);
            else if (parts[index].transform.position.x < -3)
                parts[index].transform.position = Vector3.Lerp(parts[index].transform.position, new Vector3(templates[index].transform.position.x - 3, templates[index].transform.position.y), Time.deltaTime * 10f);
            else if (parts[index].transform.position.x > -1)
                parts[index].transform.position = Vector3.Lerp(parts[index].transform.position, new Vector3(templates[index].transform.position.x + 3, templates[index].transform.position.y), Time.deltaTime * 10f);

        }
        if (parts[index].transform.position == new Vector3(templates[index].transform.position.x, templates[index].transform.position.y) || parts[index].transform.position == new Vector3(templates[index].transform.position.x + 3, templates[index].transform.position.y )|| parts[index].transform.position == new Vector3(templates[index].transform.position.x-3, templates[index].transform.position.y))
        {
            if (index < templates.Length - 1)
                index++;
            isDropped = false;
            parts[index].SetActive(true);
        }
    }
}
 
  

