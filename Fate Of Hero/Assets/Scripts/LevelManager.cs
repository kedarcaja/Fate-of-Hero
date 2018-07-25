using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
	
    [SerializeField]
    private float autoLoadNextLevel;

    private LevelManager levelManager;

    public LevelManager MyInstance
    {
        get
        {
            return levelManager;
        }

        set
        {
            levelManager = value;
        }
    }

    void Start() {
        if (autoLoadNextLevel != 0) 
        {Invoke("LoadNextLevel", autoLoadNextLevel);}  
    }

   public void LoadLevel(string name)
   {   
       Debug.Log("New Level load: " + name);
        Application.LoadLevel(name);
        MusicManager.MusicEnd = true;
    }

   public void LoadNextLevel()
   {
        Application.LoadLevel( Application.loadedLevel + 1);
   }
}
