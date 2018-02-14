using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mracek : MonoBehaviour {
    #region Varibles

    [SerializeField]
    private float speed;

    private Vector3 startPos;


    #endregion

    #region Unity methods

    void Start() {
        startPos = transform.position;
    }


    void Update() {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnBecameInvisible()
    {

        transform.position = startPos;
    }
   #endregion
}
