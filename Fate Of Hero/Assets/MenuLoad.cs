using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour {


    private GameObject[] OtherMenuScenes;
    [SerializeField]
    private GameObject ActiveScene;
    private Button ThisGameObjectButton;


  
    private void Awake()
    {

      
        ActiveScene.tag = "Untagged";
        OtherMenuScenes = GameObject.FindGameObjectsWithTag("MenuScreen");
      

        ThisGameObjectButton = GetComponent<Button>();
    }
    private void Update()
    {
        ThisGameObjectButton.onClick.AddListener(() => ChangeScene());
     
    }



    public void ChangeScene()
    {
       

        for (int i = 0; i < OtherMenuScenes.Length; i++)
        {

            OtherMenuScenes[i].SetActive(false);
            
        }
        ActiveScene.SetActive(true);

       
    }

    public void Back()
    {

      
        transform.parent.transform.parent.gameObject.SetActive(false);
      



    }
  




}
