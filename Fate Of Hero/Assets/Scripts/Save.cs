using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {
	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
		{
			PlayerPrefs.SetFloat("Playerpos.X", transform.position.x);
			PlayerPrefs.SetFloat("Playerpos.Y", transform.position.y);
			PlayerPrefs.SetFloat("Playerpos.Z", transform.position.z);

			Vector2 v2Velocity = rb.velocity; 
			PlayerPrefs.SetFloat ("Vel.X", v2Velocity.x);
			PlayerPrefs.SetFloat ("Vel.Y", v2Velocity.y);


		}
		if(Input.GetKeyDown(KeyCode.Y))
		{
			transform.position = new Vector3(
				PlayerPrefs.GetFloat("Playerpos.X"), 
				PlayerPrefs.GetFloat("Playerpos.Y"),
				PlayerPrefs.GetFloat("Playerpos.Z")
			);
			rb.velocity = new Vector2(
				PlayerPrefs.GetFloat("Vel.X"),
				PlayerPrefs.GetFloat("Vel.Y")

			);
		}
	}
}
