using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chain : MonoBehaviour
{



    
    private bool canMove = false;
    private bool returnToStart = false;
    [SerializeField]
   private Button Grab;
    [SerializeField]
    private float speed;
   
    private float startX,startY;
    private void Start()
    {
        startX = -7749f;
        startY = 4642f;
   
      
    }
    private void Update()
    {


        if (canMove)
        {


            transform.position = new Vector3(transform.position.x, Input.mousePosition.y);
          

        }
        Grab.onClick.AddListener(()=>OnClick());
        if (returnToStart)
            ReturnToStartPosition();
       
    }



private void OnClick()
    {
       
        canMove = true;



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canMove = false;
        returnToStart = true;
        

    }
    private void ReturnToStartPosition()
    {

        transform.localPosition = new Vector3(startX, Mathf.Lerp(transform.localPosition.y, startY,Time.deltaTime*speed));
    }
}