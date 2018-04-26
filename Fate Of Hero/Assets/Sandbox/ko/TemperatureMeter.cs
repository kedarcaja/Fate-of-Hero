using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureMeter : MonoBehaviour {
    [SerializeField]
    private float max,ValZ;
    public static float speed;
    private Vector3 v3To = Vector3.zero;
    private Vector3 Angels;
   [SerializeField]
    private bool CanTurn = true;
    public static bool HasStarted { get; set; }
    [SerializeField]
    private GameObject Bellow;
    private RectTransform myRectTransform;
 private void Awake()
     {
       
        speed = 2;
         myRectTransform = GetComponent<RectTransform>();

     }

     private void Update()
     {
         Angels = myRectTransform.localEulerAngles;
            v3To.z = Angels.z +ValZ;

        if (CanTurn&&HasStarted)
        {
            Bellow.SetActive(true);
            Angels = Vector3.Lerp(Angels, v3To, Time.deltaTime * speed);
            myRectTransform.localEulerAngles = Angels;
        }
           
        
        if (Angels.z <= max)
            CanTurn = false;
        print(speed);
    }


  

   
}
