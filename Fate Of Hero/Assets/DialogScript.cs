using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogEditor;

public class DialogScript : MonoBehaviour
{
    [SerializeField]
    private DialogGraph graph;

    [SerializeField]
    private bool playOnTrigger = true, playOnAwake = false;

   

    public DialogGraph Graph => graph;

    private void OnTriggerEnter(Collider other)
    {
        if (playOnTrigger)
        {
            DialogManager.Instance.ChangeGraph(this);
            DialogManager.Instance.Play();
        }
    }


    private void Start()
    {
        if (playOnAwake)
        {
            DialogManager.Instance.ChangeGraph(this);
            DialogManager.Instance.Play();
        }
    }
}
