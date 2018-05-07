using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallFaded : MonoBehaviour
{
    [SerializeField]
    private GameObject wall1, wall2;
    private Color def;
    private Color fade;


    void Start()
    {
        def = wall1.GetComponent<SpriteRenderer>().color;
        fade = def;
        fade.a = 0.4f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wall1.GetComponent<SpriteRenderer>().color = fade;
            wall2.GetComponent<SpriteRenderer>().color = fade;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wall1.GetComponent<SpriteRenderer>().color = def;
            wall2.GetComponent<SpriteRenderer>().color = def;
        }
    }
}
