using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    private Piece activePeace;
    private Animator anim;
	void Start () {
        activePeace = FindObjectOfType<Piece>();
        anim = GetComponent<Animator>();
	}
	
	
	void Update () {
        if (activePeace.ActivePiece != null && Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = activePeace.ActivePiece.transform.position;
            anim.SetTrigger("Hit");
            if(!activePeace.IsEmpty())
            activePeace.RemoveImage();
           

        }
      
    }
}
