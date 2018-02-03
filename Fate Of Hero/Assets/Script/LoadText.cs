using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour {
  
    public TextAsset TextFile;
    public Text CreditText;
    public int Ymax;
    public PlayAnim Anim;
  
    RectTransform myRectTransform;
    public bool IsPlay;
    public float speed;
	
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
           
            Anim.Press();
            myRectTransform.localPosition = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          
            Anim.Press();
            myRectTransform.localPosition = new Vector3(0, 0, 0);

        }

    }

    private void OnGUI()
    {
        CreditText.text = TextFile.text;
    }

    
}
