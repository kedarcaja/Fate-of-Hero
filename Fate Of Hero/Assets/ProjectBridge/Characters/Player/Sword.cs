using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGames
{
    public class Sword : MonoBehaviour
    {

        private bool isGivingDamage = false;
        [SerializeField]
        private float damage;
        public Collider owner;
        public void EnableDamage()
        {
            isGivingDamage = true;
        }
        public void DisableDamage()
        {
            isGivingDamage = false;
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy") && isGivingDamage)
            {
                Debug.Log("damaged");

                other.transform.GetComponent<PlayerScript>().TakeDamage(damage, owner.transform);
            }
        }
    }
}