using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInterpret : MonoBehaviour {
    public Dialog dialog;
    public bool IsEnable;
  
    private void Awake()
    {
        dialog.Init();// filling the default delegates
        dialog.OnEnd += () => { Destroy(gameObject); };
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" &&IsEnable)
        {
            dialog.OnStart();

        }
    }

}

