using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


    #region Variables
    private static PlayerScript instance;
    public float speed = 5f;
    public bool isMove;

    public static PlayerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerScript>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    #endregion

    #region Unity Metod

    void Start()
    {
        isMove = true;
    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * UnityEngine.Time.deltaTime, Input.GetAxis("Vertical") * speed * UnityEngine.Time.deltaTime, 0f);
        }
       
    }
    #endregion
}