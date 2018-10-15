using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : Character
{
    private static PlayerScript instance;
   
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
        base.Awake();
       

    }
    protected override void Start()
    {

        base.Start();
    }
    protected override void Update()
    {

        base.Update();
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
