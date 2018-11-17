using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Transform player;
    public Transform Up;
    public Transform Down;
    public bool Where;

    private void Awake()
    {
        InvokeRepeating("RotateTarget", 0, 1f);
    }
    void RotateTarget()
    {

        if (Vector3.Distance(Up.position, player.position)< Vector3.Distance(Down.position, player.position))
        {
            Where = true;
        }
        else
        {
            Where = false;
        }


    }
}
