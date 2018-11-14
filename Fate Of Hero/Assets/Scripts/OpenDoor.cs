using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    public enum Mode { unlocked, locked }
    private float Range = 2;
    Animator anim;
    bool IsOpen;
    bool Key,trigger;
    public Mode mode;
    Transform player;

    void Start()
    {
        anim = GetComponent<Animator>();
        Key = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < Range)
        {
            trigger = true;
        }
        else
        {
            trigger = false;
        }


        if (Input.GetKeyDown(KeyCode.E) && trigger&&mode==Mode.unlocked)
        {
            Open();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Open");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            anim.SetTrigger("Close");
        }
    }
    
    public void Open()
    {
        switch (mode)
        {
            case Mode.unlocked:
                if (!IsOpen)
                {
                    anim.SetTrigger("Open");
                    IsOpen = true;
                }
                else
                {
                    anim.SetTrigger("Close");
                    IsOpen = false;
                }
                break;
            case Mode.locked:
                if (!IsOpen && Key)
                {
                    anim.SetTrigger("Open");
                    IsOpen = true;
                    mode = Mode.unlocked;
                }
                else
                {
                    anim.SetTrigger("Close");
                    IsOpen = false;
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 1f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0, transform.position.y - 1, transform.position.z + 0), Range);
    }
    public void Unlock()
    {
        mode = Mode.unlocked;
    }
}