using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> dontDestroyOnLoadObjects;

    private void Awake()
    {
        for (int i = 0; i < dontDestroyOnLoadObjects.Count; i++)
        {
            DontDestroyOnLoad(dontDestroyOnLoadObjects[i]);
        }
    }
}
