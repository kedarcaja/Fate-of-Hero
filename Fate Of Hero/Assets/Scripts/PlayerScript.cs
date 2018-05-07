using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
   
    private static PlayerScript instance;
    [SerializeField]
    private float speed;

    protected Vector2 direction;
    public bool isMove;
    private Rigidbody2D myrigidbody;

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

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        isMove = true;
    }

    void Update()
    {
        GetInput();
        if (isMove)
        {
            Move();
        }
       
    }
    public void Move()
    {
        myrigidbody.velocity = direction.normalized * speed;
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down; 
        }
        if (Input.GetKey(KeyCode.A))
        {  
            direction += Vector2.left;  
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

    }
}