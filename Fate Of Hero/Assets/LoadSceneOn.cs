using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOn : MonoBehaviour
{
    [SerializeField]
    private bool triggerEnter = true, collisionEnter;
    [SerializeField]
    private string sceneName;


    private void OnTriggerEnter(Collider other)
    {
        if (triggerEnter && other.tag == "Character" && other.transform.root.name == "Leo")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
