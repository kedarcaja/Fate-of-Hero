using UnityEngine;
using FourGames;

public class MiniMapa : MonoBehaviour
{
    [SerializeField]
    private float y;
    private Transform character;

    void Update()
    {
        if (!character)
        {
            character = FindObjectOfType<PlayerScript>().GetComponent<Transform>();
        }

        transform.position = new Vector3(character.transform.position.x, character.transform.position.y + y, character.transform.position.z);
    }
}

