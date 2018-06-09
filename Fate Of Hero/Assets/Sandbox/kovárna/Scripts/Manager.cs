using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    [SerializeField]
    private GameObject[] templates,parts;
    private Color clr = Color.white;
    private int index = 0;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Camera myCamera;
   private bool isDropped = false;
	
	void Start () {
        clr.a = 0;
        for (int i = 0; i < templates.Length; i++)
        {
            templates[i].GetComponent<SpriteRenderer>().color = clr;
            parts[i].SetActive(false);

        }
        parts[index].SetActive(true);

    }


    void Update () {
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
            parts[index].transform.position = Vector3.Lerp(parts[index].transform.position, templates[index].transform.position,Time.deltaTime*10f);
          
        }
        if (templates[index].transform.position == parts[index].transform.position)
        {
            if(index<templates.Length-1)
            index++;
            isDropped = false;
            parts[index].SetActive(true);
        }
    }
 
  
}
