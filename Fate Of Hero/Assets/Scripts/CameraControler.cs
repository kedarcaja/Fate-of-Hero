using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    #region Variables
    public GameObject Player;
    public Vector3 offset;
    #endregion

    #region Unity Metod
    private void Awake()
    {
       
        offset = transform.position - Player.transform.position;
        transform.position = Player.transform.position + offset;
    }

 
	
	void Update () {
        transform.position = Player.transform.position + offset;
	}
  #endregion  
}
