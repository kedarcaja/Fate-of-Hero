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
        OtherMenuScenes = GameObject.FindGameObjectsWithTag("MenuScreen");
        ActiveScene.tag = "Untagged";
        ThisGameObjectButton = GetComponent<Button>();
    }
    private void Update()
    {
        ThisGameObjectButton.onClick.AddListener(() => ChangeScene());
    }



    public void ChangeScene()
    {
        

        for(int i = 0; i < OtherMenuScenes.Length; i++)
        {

            OtherMenuScenes[i].SetActive(false);
            
        }
    ActiveScene.SetActive(true);

    }





}
