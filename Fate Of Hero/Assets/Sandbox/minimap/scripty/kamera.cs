using UnityEngine;

public class kamera : MonoBehaviour
{
    public Transform hrac;
    int x = 0, y = 2, z = -2;
    void Update()
    {
        transform.position = hrac.position + new Vector3(x, y, z);
    }
}
