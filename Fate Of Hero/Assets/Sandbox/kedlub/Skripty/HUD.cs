using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Toggle randomToggle;
    public NPC npc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(randomToggle.isOn)
        {
            npc.randomMove = true;
        }
        else
        {
            npc.randomMove = false;
        }
	}
}
