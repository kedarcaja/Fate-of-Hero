using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour {

    public void Loadlevel()
    {
        SceneManager.LoadScene("UvodniLokace");
        Debug.Log("přesun do UvodniLokace");
    }

    public void QuitGame()
    {
        Debug.Log("Konec");
        Application.Quit();
    }
}
