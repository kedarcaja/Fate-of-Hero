using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    public Transform hrac;
   
    public int y;
    
    void Update()
    {
        transform.position = hrac.transform.position + new Vector3(0, 50, 0);
    }
}

