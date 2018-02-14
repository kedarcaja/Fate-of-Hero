using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Varibles
    [SerializeField]
    private stats health;
    #endregion

    #region Unity methods

    private void Awake()
    {
        health.Initialize();
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentVal -= 10;
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentVal += 10;
        }

    }
    #endregion
}
