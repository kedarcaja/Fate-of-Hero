using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
  
    [SerializeField]
    private int zivor;


    [SerializeField]
    private Stats health;
  
   



    private void Awake()
    {
        health.Initialize();
       

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.CurrentVal -= 10;
          

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            health.CurrentVal += 10;
         
        }

    }
}
