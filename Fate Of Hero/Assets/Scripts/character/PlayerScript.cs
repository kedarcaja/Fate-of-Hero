using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : Character
{
    private static PlayerScript instance;
    public bool CanMove;
    public static PlayerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerScript>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    protected override void Awake()
    {
        CanMove = true;
        base.Awake();

       

    }
    
    protected override void Update()
    {
        if(CanMove)
        {
            base.Update();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Health.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Health.CurrentVal += 10;
        }
    }

    private void GetInput()
    {
  
    }
}
