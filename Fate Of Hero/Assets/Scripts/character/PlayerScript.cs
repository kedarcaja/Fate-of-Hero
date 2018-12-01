using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : Character
{
    private static PlayerScript instance;
    [SerializeField]
    protected Stats Stamina;
    [SerializeField]
    private float playerSpeed;
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
        AgentSpeed = playerSpeed;
        if (Stamina.Bar != null)
        {
            Stamina.Initialize();
        }
        base.Awake();
    }
    
     void Update()
    {
        if(CanMove)
        {
           
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MyAgent.speed = AgentSpeed * 2;
                Move();
                Stamina.CurrentVal -= 10;
            }
            else
            {
                MyAgent.speed = AgentSpeed;
                Move();
                
            }
        }
//pouze pro testování fukčnosti
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Health.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Health.CurrentVal += 10;

        }
///
    }
    protected override void Move()
    {
        base.Move();
    }
     public void GetInput()
    {
        
    }
}
