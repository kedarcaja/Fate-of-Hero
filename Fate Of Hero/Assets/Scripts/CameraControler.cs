using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    #region Variables
   
    private GameObject player;
    [SerializeField]
    private Vector3 offset;
    #endregion

    #region Unity Metod
    private void Awake()
    {
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
    }
    private void Start()
    {
      
        player = GameObject.FindWithTag("Player");
    }


    void Update () {
        transform.position = player.transform.position + offset;
	}
  #endregion  
}
