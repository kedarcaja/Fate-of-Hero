using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BarrelwithKey : MonoBehaviour {

    private float Range = 1;
    bool trigger;
    Transform player;
    public UnityEvent OnEvent;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null && Vector3.Distance(transform.position, player.position) < Range)
        {
            trigger = true;
        }
        else if (trigger && Vector3.Distance(transform.position, player.position) > Range)
        {
            trigger = false;
        }

        if (Input.GetMouseButtonDown(0) && trigger)
        {
            OnEvent.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 1f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0, transform.position.y - 1, transform.position.z + 0), Range);
    }
}
