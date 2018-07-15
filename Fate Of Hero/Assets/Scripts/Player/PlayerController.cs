using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float MovementSpeed = 2f;

    public bool IsMove;
    

    private Rigidbody rigid;
    Animator anim;

    
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3();
        if (IsMove)
        {

            if (Input.GetKey(KeyCode.W))
            {
                velocity += Vector3.forward * (MovementSpeed * 100) * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                velocity += -Vector3.forward * (MovementSpeed * 100) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity += Vector3.left * (MovementSpeed * 100) * Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.D))
            {
                velocity += -Vector3.left * (MovementSpeed * 100) * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        if (velocity == Vector3.zero)
        {
            anim.SetBool("Moving", false);
        }
        else
        {

            transform.rotation = Quaternion.LookRotation(velocity);
            anim.SetBool("Moving", true);
        }
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;

    }
}
