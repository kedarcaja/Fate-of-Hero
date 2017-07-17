using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour {

    public void Loadlevel()
    {
        SceneManager.LoadScene("UvodniLokace");
        Debug.Log("As you wish! :)");
    }

    public void QuitGame()
    {
        Debug.Log("As you wish! :)");
        Application.Quit();
    }
}
