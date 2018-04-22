using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuScene : MonoBehaviour {

    private GameObject Deactive;
    [SerializeField]
    private GameObject Menu,MenuPanel;
    private Color StartColor = new Color(0,0,0,255);
  private GameObject Active;
    [SerializeField]
    private CreditsScript Credits;
  
    public void SceneChangeOnClick(GameObject activeScene)
    {
        Active = activeScene;
        ChangeScene();
        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Active != Menu||Credits.isOnEnd())
        {

            Active = Menu;
            ChangeScene();
         
        }
    }



 private void ChangeScene()
    {

     
        MenuPanel.SetActive(true);
        for (int i = 0; i < GameObject.Find("MenuScenes").transform.childCount; i++)
        {
            Deactive = GameObject.Find("MenuScenes").transform.GetChild(i).gameObject;
            if (Deactive != Active)
                Deactive.SetActive(false);


        }
        StartCoroutine(waitFade());

        Active.SetActive(true);
        Credits.ResetTextPosition();
       

      

       


    }


    IEnumerator waitFade()
    {


        yield return new WaitForSeconds(0.5f);
        MenuPanel.GetComponent<Image>().color = StartColor;
        MenuPanel.SetActive(false);



    }

}
