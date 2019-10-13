using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;
    int x = 0, y = 2, z = -2;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        transform.position = new Vector3(x, y, z);
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
