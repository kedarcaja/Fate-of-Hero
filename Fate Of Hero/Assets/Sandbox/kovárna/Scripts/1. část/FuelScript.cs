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
    private GameObject Ore,Fuel;
    private ParticleSystem Fire;
 
    private void Awake()
    {
        FuelButton = GetComponent<Button>();
        TemperatureMeter.HasStarted = false;
        choosed = false;
        Ore = GameObject.Find("rudy");
        Fuel = GameObject.Find("Paliva");
        Fire = FindObjectOfType<ParticleSystem>();
        Fire.Stop();
        speed = 500;
    }
    private void Start()
    {
        Ore.SetActive(false);
    }
    void Update () {
        if (isInInventory&&!TemperatureMeter.HasStarted)
        {
            FuelButton.interactable = true;
        }

        if (!isInInventory) { FuelButton.interactable = false; }
       

        if (choosed)
        {
            for(int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).gameObject != gameObject)
                    transform.parent.GetChild(i).gameObject.SetActive(false);
            }
            if(transform.position == target.position)
            {
             if(Ore.activeSelf)
                    Fuel.SetActive(false);

                Ore.SetActive(true);
               
                Fire.Play();
            }
            SetDestination();


            if (gameObject.transform.IsChildOf(Ore.transform))
            {
                if(transform.position == target.position)
                Ore.SetActive(false);

                HasStarted();
            }
          
        }
        if (transform.IsChildOf(Fuel.transform))
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
        print("clicked");
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
