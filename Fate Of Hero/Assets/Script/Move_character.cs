using UnityEngine;
using System.Collections;

public class Move_character : MonoBehaviour {


    #region Variables
    public float speed = 5f;
    #endregion

    #region Unity Metod

    void Start()
    {

    }

    void Update()
    {

        transform.Translate(Input.GetAxis("Horizontal") * speed * UnityEngine.Time.deltaTime, Input.GetAxis("Vertical") * speed * UnityEngine.Time.deltaTime, 0f);
    }
    #endregion
}