using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour {


    private GameObject[] OtherMenuScenes;
    [SerializeField]
    private GameObject ActiveScene;
    private Button ThisGameObjectButton;
    private PanelDeactive[] Panels;
  
  
    private void Awake()
    {

        Panels = FindObjectsOfType<PanelDeactive>();
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

          
#pragma warning restore CS0618 // Typ nebo člen je zastaralý.

        }
        for(int i = 0; i < Panels.Length; i++)
        {


            Panels[i].ResetPanel();
        }
        ActiveScene.SetActive(true);
       
       
    }

    public void Back()
    {

      
        transform.parent.transform.parent.gameObject.SetActive(false);
      



    }
  

}
