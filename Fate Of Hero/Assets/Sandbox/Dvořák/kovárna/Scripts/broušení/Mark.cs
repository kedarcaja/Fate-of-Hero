using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mark : MonoBehaviour {
    
    private float markHealth,startHealth;
    [SerializeField]
    private Text markText;
    [SerializeField]
    private Image mark;
    int myIndex = 0;
  
    void Start () {
        startHealth =150;
        markHealth = startHealth;

        markText.color = Color.white;
        mark.color = Color.red;
        print(gameObject.name+" "+myIndex);
    }


    void Update () {
        markText.text =Percentile() + "%";
     
  
	}
    private void OnTriggerEnter(Collider other)
    {

      


        if (MarkIsNotDestroyedAndWheelIsRotating())
        {
            if (Wheel.Speed < -5) {

                markHealth--;

            }

            else if (Wheel.Speed>-5)
            {

                markHealth -= 0.5f;
                

            }
            onValueChange();


        }
    }
    private float Percentile()
    {
        return Mathf.Round((markHealth / startHealth) * 100);
    }

    private bool MarkIsNotDestroyedAndWheelIsRotating()
    {


        return markHealth > 0 && Wheel.isRotating;
    }
   private Color LerpColor(Color b)
    {


            return mark.color = Color.Lerp(mark.color, b, Time.deltaTime);
      

    }

    private void onValueChange()
    {
        
        Color[] colors = {Color.red, Color.blue, Color.yellow,Color.magenta, Color.green };
        if (Percentile() % 28 == 0&& myIndex<colors.Length-1)
        {
            myIndex++;
           
        }



        LerpColor(colors[myIndex]);
    }
}
