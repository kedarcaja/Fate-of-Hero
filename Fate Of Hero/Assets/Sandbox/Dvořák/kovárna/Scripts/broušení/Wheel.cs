using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
[SerializeField]
    private  static float speed,maxSpeed;
    private ParticleSystem PS;
    public static bool isRotating;
   public  static float Speed
    {


        get { return speed; }
      private  set
        {

            value = speed;
        }
    }
   
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
        if (speed < 0 && swordIsInContact)
        {
            isRotating = true;
            PS.Play();
        }

        else
        {
            PS.Stop();
            isRotating = false;
        }

       
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
