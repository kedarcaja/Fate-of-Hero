using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologInterpret : MonoBehaviour {

    public Monolog monolog;
    public bool IsEnable;
    private void Awake()
    {
        monolog.Init();// filling the default delegates
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DialogTrigger"&&IsEnable)
        {
            monolog.OnStart();

        }
    }
}
