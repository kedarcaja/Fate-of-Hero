using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {

    public Animator anim;
    public CharacterController controller;
    public NavMeshAgent agent;
    float timer;
    float timerMax;
    //bool shouldMove = false;
    public bool randomMove = true;
    bool canMove = true;
    float move;
    float moveMax;
    Vector3 oldQuat;
    Vector3 newQuat;

	// Use this for initialization
	void Start () {
        //oldQuat = this.transform.eulerAngles;
        //newQuat = this.transform.eulerAngles;
        timerMax = Random.Range(1, 10);
	}

    public void Move(Vector3 dest)
    {
        canMove = false;
        agent.SetDestination(dest);
    }
	
	// Update is called once per frame
	void Update () {
        if (randomMove && canMove)
        {
            timer += Time.deltaTime;
        }
        if(timer >= timerMax && canMove)
        {
            canMove = false;

            agent.SetDestination(this.transform.position + new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)));
            //move = 0;
            //moveMax = Random.Range(1, 7);
            //oldQuat = this.transform.rotation;
            //newQuat = oldQuat * Quaternion.Euler(new Vector3(0,Random.Range(0,180),0));
            //oldQuat = this.transform.eulerAngles;
            //newQuat = oldQuat + new Vector3(0, Random.Range(0, 180), 0);
            //anim.SetBool("move", true);
        }

        if(agent.remainingDistance < 0.1)
        {
            anim.SetFloat("Speed", 0);
            anim.SetBool("move", false);
            if (!canMove)
            {
                timer = 0;
                canMove = true;
            }
        }
        else
        {
            anim.SetFloat("Speed", agent.speed);
            anim.SetBool("move", true);
        }

        /*if(shouldMove)
        {
            move += Time.deltaTime;
            controller.SimpleMove(this.transform.forward * 2f);
            if(move >= moveMax)
            {
                anim.SetBool("move", false);
                timer = 0;
                shouldMove = false;
                move = 0;
            }
        }*/

        //this.transform.eulerAngles = Vector3.Lerp(oldQuat, newQuat, 0.5f);

	}
}
