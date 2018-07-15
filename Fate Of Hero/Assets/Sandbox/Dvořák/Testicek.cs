using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testicek : MonoBehaviour {

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("Walk 1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("Walk 2");
        }
    }
}
