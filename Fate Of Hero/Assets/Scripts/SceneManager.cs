using System;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    [Header("Help")]
    [SerializeField]
    private Text sizeTextObject;
    [SerializeField]
    private Text visialTextObject;

    internal static void LoadScene(int v)
    {
        throw new NotImplementedException();
    }

    [SerializeField]
    private Text descriptionText;
    private Vector3 keyStartPosition;

    public GameObject HelpObject;

    public RectTransform rect;

    public Text VisialTextObject
    {
        get
        {
            return visialTextObject;
        }


    }

    public Text DescriptionText
    {
        get
        {
            return descriptionText;
        }


    }

    public Text SizeTextObject
    {
        get
        {
            return sizeTextObject;
        }


    }

    internal static object GetActiveScene()
    {
        throw new NotImplementedException();
    }

    public static SceneManager Instance;

    private void Awake()
    {
        Instance = FindObjectOfType<SceneManager>();
        keyStartPosition = sizeTextObject.transform.position;
    }

    public void ShowHelp()
    {
        HelpObject.gameObject.SetActive(true);

        descriptionText.transform.position = new Vector2(descriptionText.transform.position.x + sizeTextObject.text.Length, descriptionText.transform.position.y);
        sizeTextObject.text = " " + visialTextObject.text + " ";
    }

    public void HideHelp()
    {

        HelpObject.gameObject.SetActive(false);
    }
}
