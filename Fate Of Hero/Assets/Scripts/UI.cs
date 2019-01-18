using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject text;

    public void Enter()
    {
        Debug.Log("kuk");
        text.SetActive(true);
    }

    public void Exit()
    {
        text.SetActive(false);
    }
}
