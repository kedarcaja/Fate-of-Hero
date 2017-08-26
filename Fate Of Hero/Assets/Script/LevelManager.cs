using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
	
    public float autoLoadNextLevel;
   void Start() {
        if (autoLoadNextLevel == 0) {Debug.Log("Level auto load Disable");}
        else{Invoke("LoadNextLevel", autoLoadNextLevel);}
    }

   public void LoadLevel(string name)
   {
       Debug.Log("New Level load: " + name);
       Application.LoadLevel(name);
   }

   public void LoadNextLevel()
   {
       Application.LoadLevel(Application.loadedLevel + 1);
   }

      

  
	
}
