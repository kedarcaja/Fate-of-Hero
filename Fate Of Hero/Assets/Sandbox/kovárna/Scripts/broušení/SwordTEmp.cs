using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTEmp : MonoBehaviour {

    private Animator anim;
    private Wheel wheel;
    [SerializeField]
  
	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        wheel = FindObjectOfType<Wheel>();

    }

    // Update is called once per frame
    void Update () {
		

        if(Wheel.swordIsInContact&&wheel.Speed<0)
        {
            anim.enabled = true;
        }
        else
            anim.enabled = false;
    }
}
