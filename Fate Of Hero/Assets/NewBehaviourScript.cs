using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {


    private GameObject[] MenuScenes;

    private void Awake()
    {
        MenuScenes = GameObject.FindGameObjectsWithTag("MenuScreen");
    }



 
 public void ChangeScene(GameObject ActiveMenuScene)
    {
        

        for(int i = 0; i < MenuScenes.Length; i++)
        {
          
            if(MenuScenes[i] != ActiveMenuScene)
            {


                MenuScenes[i].SetActive(false);
            }
        }
        ActiveMenuScene.SetActive(true);

    }





}
