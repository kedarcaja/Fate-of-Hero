using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    private bool onCD;
  
    private void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CombatTextManager.Instance.CreateText(transform.position,"Hello",Color.red,false);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CombatTextManager.Instance.CreateText(transform.position, "Hello", Color.red,true);
        }

        transform.Translate(Input.GetAxis("Horizontal") * speed * UnityEngine.Time.deltaTime, Input.GetAxis("Vertical") * speed * UnityEngine.Time.deltaTime, 0f);

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Fire")
        {
            if (!onCD)
            {
                StartCoroutine(CoolDownDamage());
                int random = Random.Range(0, 5);
                if (random <= 3)
                {
                    int rndDmg = Random.Range(3, 10);
                    CombatTextManager.Instance.CreateText(transform.position, "-" + rndDmg.ToString(), Color.red, false);
                }
                else
                {
                    int rndDmg = Random.Range(11, 20);
                    CombatTextManager.Instance.CreateText(transform.position, "-" + rndDmg.ToString(), Color.red, true);
                }
            }
           
        }
        if (other.name == "Heal")
        {
            if (!onCD)
            {
                StartCoroutine(CoolDownDamage());
                int random = Random.Range(0, 5);
                if (random <= 2)
                {
                    int rndDmg = Random.Range(3, 10);
                    CombatTextManager.Instance.CreateText(transform.position, "+" + rndDmg.ToString(), Color.green, false);
                }
                else
                {
                    int rndDmg = Random.Range(11, 20);
                    CombatTextManager.Instance.CreateText(transform.position, "+" + rndDmg.ToString(), Color.green, true);
                }
            }
         
        }
    }

    private IEnumerator CoolDownDamage()
    {
        onCD = true;
        yield return new WaitForSeconds(1);
        onCD = false;
        
    }
}
