using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallFaded : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wall;
    private Color def;
    private Color fade;


    void Start()
    {
        def = wall[0].GetComponent<SpriteRenderer>().color;
        fade = def;
        fade.a = 0.4f;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<SpriteRenderer>().color = fade;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<SpriteRenderer>().color = def;
            }
        }
    }
}
