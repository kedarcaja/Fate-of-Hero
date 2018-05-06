using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour
{
    private static CombatTextManager instance;
    public static CombatTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    public static float health;
    public GameObject TextPrefab;
    public RectTransform canvas;
    public Transform Object;
    public float speed;
    public float fadeTime;
    public Vector3 direction;

    public void CreateText(Vector3 position, string text,Color color, bool crit)
    {
         GameObject sct = (GameObject)Instantiate(TextPrefab, position, Quaternion.identity);
        sct.transform.SetParent(canvas);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        sct.GetComponent<RectTransform>().position = Object.transform.position;
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime, crit);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;
    }
}
