using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonologInterpret : MonoBehaviour {

    public Monolog monolog;
    public bool IsEnable;
    public UnityEvent OnMonologEnd;
    public UnityEvent OnMonologStart;
    private void Awake()
    {
        monolog.Init();// filling the default delegates
        monolog.OnEnd += () =>
        {
            Destroy(gameObject);

            OnMonologEnd.Invoke();
        };
        monolog.OnStart += () => { OnMonologStart.Invoke(); };
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DialogTrigger"&& IsEnable)
        {
            monolog.OnStart();

        }
    }
}
