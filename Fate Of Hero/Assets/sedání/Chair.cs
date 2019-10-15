using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public GameObject character;
    Animator anim;
    bool isWalkingTowards;
    bool sittingOn = false;

    private void OnMouseDown()
    {
        if (!sittingOn)
        {
            isWalkingTowards = true;
            anim.SetTrigger("isWailking");

        }
    }
        private void Awake()
        {
            anim = character.GetComponent<Animator>();
        }
    
    void Update()
    {
        if (isWalkingTowards == true)
        {
            Vector3 targetDir;
            targetDir = new Vector3(transform.position.x - character.transform.position.x, 0f,
               transform.position.z - character.transform.position.z);

            Quaternion rot = Quaternion.LookRotation(targetDir);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);
            character.transform.Translate(Vector3.forward * 0.01f);

            if (Vector3.Distance(character.transform.position, transform.position) < 0.6)
            {
                anim.SetTrigger("isSitting");
                character.transform.rotation = transform.rotation;
                isWalkingTowards = false;
                sittingOn = true;
            }
        }
    }
}
