using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallFaded : MonoBehaviour {
    #region Varibles
    [SerializeField]
    private GameObject wall;

    private Color def;
    private Color fade;
   #endregion
   
   #region Unity methods
  
	void Start () {
        def = wall.GetComponent<SpriteRenderer>().color;
        fade = def;
        fade.a = 0.7f;
	}
	
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wall.GetComponent<SpriteRenderer>().color = fade;
        }
        
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            wall.GetComponent<SpriteRenderer>().color = def;
        }
    }
    #endregion
}
