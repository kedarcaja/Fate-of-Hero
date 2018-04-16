using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour {

    private  GameObject[] OtherMenuScenes;
    private  Button ThisGameObjectButton;
    private  PanelDeactive[] Panels;
    [SerializeField]
    private GameObject scene;

    public GameObject Scene
    {
        get
        {
            return scene;
        } 
    }

    private void Awake()
    {

        Panels = FindObjectsOfType<PanelDeactive>();  
        OtherMenuScenes = GameObject.FindGameObjectsWithTag("MenuScreen");
        ThisGameObjectButton = GetComponent<Button>();
    }

    public  void ChangeScene(GameObject activeScene)
    {
        activeScene.tag = "Untagged";


        for (int i = 0; i < OtherMenuScenes.Length; i++)
        {
            OtherMenuScenes[i].SetActive(false);
        }
        for(int i = 0; i < Panels.Length; i++)
        {
            Panels[i].ResetPanel();
        }
      
        activeScene.SetActive(true);
        activeScene.tag = "MenuScreen";
    }

    public void BackToMenu(GameObject activeScene)
    {
        //activeScene.tag = "Untagged";


        //for (int i = 0; i < OtherMenuScenes.Length; i++)
        //{
        //    OtherMenuScenes[i].SetActive(false);
        //}
        //for (int i = 0; i < Panels.Length; i++)
        //{
        //    Panels[i].ResetPanel();
        //}
        activeScene = Scene;
        activeScene.SetActive(true);
        activeScene.tag = "MenuScreen";
    }
}
