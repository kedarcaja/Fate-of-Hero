using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
	
    public float autoLoadNextLevel;


   void Start() {
        if (autoLoadNextLevel != 0) 
        {Invoke("LoadNextLevel", autoLoadNextLevel);}
    }

   public void LoadLevel(string name)
   {
       Debug.Log("New Level load: " + name);
#pragma warning disable CS0618 // Typ nebo člen je zastaralý.
        Application.LoadLevel(name);
        MusicManager.MusicEnd = true;
#pragma warning restore CS0618 // Typ nebo člen je zastaralý.
    }

   public void LoadNextLevel()
   {

#pragma warning disable CS0618 // Typ nebo člen je zastaralý.
        Application.LoadLevel( Application.loadedLevel + 1);
#pragma warning restore CS0618 // Typ nebo člen je zastaralý.

    }

      

  
	
}
