using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    private Vector3 direction;
    public static Player Instance;
    public bool CanMove = true;
    private void Start()
    {
        Instance = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector3.zero;

        }
    }
    void Update () {
       
            GetInput();
        
        
	}
    public void Move()
    {
        rb.velocity = direction * speed*Time.deltaTime;
    }
    public void GetInput()
    {
        direction = Vector3.zero;
      
            if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction = Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction = Vector3.back;
            }
        
    }
}
