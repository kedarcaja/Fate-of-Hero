using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    #region Variables
    public GameObject Player;
    public Vector3 offset;
   #endregion
     
   #region Unity Metod
   
	void Start () {
        offset = transform.position - Player.transform.position;
	}
	
	void Update () {
        transform.position = Player.transform.position + offset;
	}
  #endregion  
}
