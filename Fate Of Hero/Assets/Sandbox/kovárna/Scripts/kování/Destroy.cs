using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    private void Update()
    {
        // Update is called once per frame
        print(HitPower.removeRust);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HitPower.removeRust)
        {

            Destroy(gameObject);
            HitPower.removeRust = false;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HitPower.removeRust = false;

    } 
}
