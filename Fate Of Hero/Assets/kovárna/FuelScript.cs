using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelScript : MonoBehaviour {
    private Button FuelButton;
    private Transform target;
    public  bool isInInventory = false;
 
    private float speed;
    private bool choosed;
    private GameObject Ore;
    private ParticleSystem Fire;
    private void Awake()
    {
        FuelButton = GetComponent<Button>();
        
        TemperatureMeter.HasStarted = false;
      
        choosed = false;
        Ore = GameObject.Find("rudy");
        Fire = FindObjectOfType<ParticleSystem>();

        Ore.SetActive(false);
        Fire.Stop();

        speed = 250;
    }
    void Update () {
        if (isInInventory&&!TemperatureMeter.HasStarted)
        {




            FuelButton.interactable = true;

        }
      
        if (!isInInventory)
            FuelButton.interactable = false;




        if (choosed)
        {
            if(transform.position == target.position)
            {


                Ore.SetActive(true);
               
                Fire.Play();
            }
            SetDestination();


            if (gameObject.transform.IsChildOf(Ore.transform))
            {

                HasStarted();
            }
          
        }
        if (transform.IsChildOf(GameObject.Find("Paliva").transform))
        {


            target = GameObject.Find("PecPos").transform;
        }
        else if (Ore.activeSelf)
        {

            target = GameObject.Find("KadPos").transform;
        }


    }



  

   public void Choosed()
    {
       
        choosed = true;

        for (int i = 0; i < transform.parent.childCount; i++)
        {


            if (transform.parent.GetChild(i) != transform)
                transform.parent.GetChild(i).gameObject.SetActive(false);
        }
       

    }
    private void SetDestination()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }

  private void HasStarted()
    {


        TemperatureMeter.HasStarted = true;
    }
  
}
