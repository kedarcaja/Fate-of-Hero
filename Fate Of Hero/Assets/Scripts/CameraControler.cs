using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Vector3 offset;
 
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
    }

    void Update () {
        transform.position = player.transform.position + offset;
	}
}
