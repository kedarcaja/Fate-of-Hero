using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour {

    [Header("Text File")]
    [SerializeField]
    private TextAsset TextFile;
    [SerializeField]
    private Text CreditText;
    [SerializeField]
    private int Ymax;
   
    [SerializeField]
    RectTransform myRectTransform;
    [SerializeField]
    private bool IsPlay;
    [SerializeField]
    private float speed;
	
	void Start () {
       myRectTransform = GetComponent<RectTransform>();     
    }
    void Update()
    {
        if (IsPlay)
        {
            myRectTransform.localPosition += new Vector3(0, (speed*10) * Time.deltaTime, 0);
        }
        if (Ymax == myRectTransform.localPosition.y)
        {
            myRectTransform.localPosition = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            myRectTransform.localPosition = new Vector3(0, 0, 0);
        }

    }

    private void OnGUI()
    {
        CreditText.text = TextFile.text;
    }

    
}
