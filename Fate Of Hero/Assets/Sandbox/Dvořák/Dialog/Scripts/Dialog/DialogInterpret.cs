using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInterpret : MonoBehaviour {
    public Dialogs dialog;
    public bool IsEnable;
    private void Awake()
    {
        dialog.Init();// filling the default delegates
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DialogTrigger" &&IsEnable)
        {
            dialog.OnStart();

        }
    }
}

