using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
[SerializeField]
    private float speed,maxSpeed;
    private ParticleSystem PS;
   
   
    public static bool swordIsInContact;


    private void Start()
    {
        PS = FindObjectOfType<ParticleSystem>();
        StartCoroutine(Delay());
        PS.Stop();
        maxSpeed = -10;
    }



    void Update () {


        transform.Rotate(new Vector3(0, -speed, 0));
        if (Input.GetKeyDown(KeyCode.Space)&& speed > maxSpeed)
            speed -= 1f;
        if (speed < 0&& swordIsInContact)
        {

            PS.Play();
        }
            
        else
            PS.Stop();

       
    }

    IEnumerator Delay()
    {
        while (true)
        {

            yield return new WaitForSeconds(1f);
            if(speed<0)
            speed += 0.5f;
        }
    }



  
}
