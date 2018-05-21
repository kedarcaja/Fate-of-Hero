using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
[SerializeField]
    private float speed;






    private void Start()
    {
        StartCoroutine(Delay());
    }



    void Update () {
        transform.Rotate(new Vector3(0, speed, 0));
        if (Input.GetKeyDown(KeyCode.Space)&& speed<=10)
            speed += 1;
    }

    IEnumerator Delay()
    {
        while (true)
        {

            yield return new WaitForSeconds(1);
            if(speed>0)
            speed -= 1f;
        }
    }
}
