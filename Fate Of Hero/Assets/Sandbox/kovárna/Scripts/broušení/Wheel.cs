using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
[SerializeField]
    private float speed;
    private ParticleSystem PS;





    private void Start()
    {
        PS = FindObjectOfType<ParticleSystem>();
        StartCoroutine(Delay());
        PS.Stop();
    }



    void Update () {
        transform.Rotate(new Vector3(0, -speed, 0));
        if (Input.GetKeyDown(KeyCode.Space)&& -speed>=-5)
            speed -= 0.5f;
        if (speed < 0)
            PS.Play();
        else
            PS.Stop();
    }

    IEnumerator Delay()
    {
        while (true)
        {

            yield return new WaitForSeconds(1);
            if(speed<0)
            speed += 0.5f;
        }
    }
}
