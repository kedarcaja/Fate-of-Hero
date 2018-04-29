using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pour : MonoBehaviour {
    private Slider Puller;
    private float CurrentValue;
    [SerializeField]
    private float speed;
    private void Start()
    {
        Puller = FindObjectOfType<Slider>();
    }

    public void TurnVat() {
        if (Puller.value > CurrentValue)
        {
            transform.Rotate(new Vector3(0, speed));
            print("+");

        }
        if (Puller.value < CurrentValue)
        {
            transform.Rotate(new Vector3(0, -speed));
            print("-");
        }

        CurrentValue = Puller.value;
        
	}
    private void Update()
    {
       
       

        
           
     

    }
}
