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
   
    private readonly int Ymax = 4700;
    
  
    [SerializeField]
    RectTransform myRectTransform;
    private float StartPosition;
    //[SerializeField]
    //private GameObject scene;
    [SerializeField]
    private float speed;
    private MenuLoad menuLoader;
  
    #endregion
    void Start()
    {
      

        myRectTransform = GetComponent<RectTransform>();
    
        StartPosition = transform.position.y;
        menuLoader = transform.parent.GetComponentInParent<MenuLoad>();
        
    }

    void Update()
    {
       if(IsPlay())
            myRectTransform.localPosition += new Vector3(0, (speed * 10) * Time.deltaTime, 0);
       
        if (Input.GetKey(KeyCode.Escape))
        {
            menuLoader.BackToMenu(menuLoader.Scene);
            ResetTextPosition();
        }
       
       
    } 
        private void OnGUI()
        {
            CreditText.text = TextFile.text;
        }
       

    private Vector3 ResetTextPosition()
    {



        return myRectTransform.localPosition = new Vector3(0,StartPosition,0);
    }

    private bool IsPlay()
    {


        return transform.parent.parent.gameObject.activeSelf;
    }
    }
 