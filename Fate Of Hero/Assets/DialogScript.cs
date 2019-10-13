using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogEditor;

public class DialogScript : MonoBehaviour
{
    [SerializeField]
    private DialogGraph graph;


    private void OnTriggerEnter(Collider other)
    {
        DialogManager.Instance.ChangeGraph(graph);
        DialogManager.Instance.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
