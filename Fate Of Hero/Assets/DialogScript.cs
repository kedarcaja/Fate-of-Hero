using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogEditor;

public class DialogScript : MonoBehaviour
{
    [SerializeField]
    private DialogGraph graph;

    public DialogGraph Graph => graph;

    private void OnTriggerEnter(Collider other)
    {
        DialogManager.Instance.ChangeGraph(this);
        DialogManager.Instance.Play();
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
