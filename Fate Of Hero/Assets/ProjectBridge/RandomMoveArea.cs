using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveArea : MonoBehaviour
{
    public float radius;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
