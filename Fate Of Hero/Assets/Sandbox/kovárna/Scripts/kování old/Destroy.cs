using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HitPower.removeRust && HItPosition.isTouching)
        {
            Destroy(gameObject);

            HitPower.removeRust = false;
        }
        HItPosition.isTouching = true;
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HitPower.removeRust && HItPosition.isTouching)
        {
            Destroy(gameObject);

            HitPower.removeRust = false;
        }
        HItPosition.isTouching = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HitPower.removeRust = false;
        HItPosition.isTouching = false;

    }
}
