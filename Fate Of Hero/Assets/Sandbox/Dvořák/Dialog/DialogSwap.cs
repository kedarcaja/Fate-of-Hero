using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSwap : MonoBehaviour {
    public GameObject next;
    private void OnDestroy()
    {
        next.SetActive(true);
    }
}
