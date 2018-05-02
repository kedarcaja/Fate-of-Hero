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
    private Vector3 Temperature;
   [SerializeField]
    private bool CanTurn = true;
    public static bool HasStarted { get; set; }
    public static float cvalityTemperature;
    [SerializeField]
    private GameObject Bellow;
    private RectTransform myRectTransform;
 private void Awake()
     {
       
        speed = 0.01f;
         myRectTransform = GetComponent<RectTransform>();

     }

     private void Update()
     {
         Temperature = myRectTransform.localEulerAngles;
            v3To.z = Temperature.z +ValZ;

        if (CanTurn&&HasStarted)
        {
            Bellow.SetActive(true);
            Temperature = Vector3.Lerp(Temperature, v3To, Time.deltaTime * speed);
            myRectTransform.localEulerAngles = Temperature;
        }
           
        
        if (Temperature.z <= max)
            CanTurn = false;
        print(speed);
    }


  

   
}
