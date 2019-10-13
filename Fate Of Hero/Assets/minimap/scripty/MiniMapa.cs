using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    public Transform hrac;
   
    public int y;
    
    void Update()
    {
        transform.position = hrac.position + new Vector3(0, y, 0);
    }
}

