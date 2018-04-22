using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    #region Variables
    [Header("Text File")]
    [SerializeField]
    private TextAsset TextFile;
    [SerializeField]
    private Text CreditText;
   
    private readonly int Ymax = 4695;
    
  
    [SerializeField]
    RectTransform myRectTransform;
    private float StartPosition;
    //[SerializeField]
    //private GameObject scene;
    [SerializeField]
    private float speed;


    #endregion
    void Start()
    {


        myRectTransform = GetComponent<RectTransform>();

        StartPosition = transform.position.y;
    }

    void Update()
    {
     
            myRectTransform.localPosition += new Vector3(0, (speed * 10) * Time.deltaTime, 0);


       
       
    } 
        private void OnGUI()
        {
            CreditText.text = TextFile.text;
        }
       

   public Vector3 ResetTextPosition()
    {



        return myRectTransform.localPosition = new Vector3(0,StartPosition,0);
    }

   
    public bool isOnEnd()
    {


        return myRectTransform.localPosition.y >= Ymax;
    }
}
 