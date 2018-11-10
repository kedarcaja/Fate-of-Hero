using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode { unlocked, locked }
public class OpenDoor : MonoBehaviour
{
    private Animator anim;
    public BoxCollider boxCollider;
    bool IsOpen;
    bool Key,trigger;
    public Mode mode;

    void Start()
    {
        anim = GetComponent<Animator>();
        Key = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&trigger)
        {
            Open();
        }
            if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Open");
           // boxCollider.enabled = false;

        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            anim.SetTrigger("Close");
          //  boxCollider.enabled = true;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = true;
            Debug.Log("enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
            trigger = false;
        Debug.Log("exit");
    }
}