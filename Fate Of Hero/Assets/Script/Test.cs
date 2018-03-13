using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    #region Varibles
    protected Vector2 direction;
 
    private Rigidbody2D myrigidbody;

    [SerializeField]
    private float speed;
    #endregion

    #region Unity methods

    void Start () {
        myrigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        GetInput();
       
            Move();
     

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
    #endregion
}
