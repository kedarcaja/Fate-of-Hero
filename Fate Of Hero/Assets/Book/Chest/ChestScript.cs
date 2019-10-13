using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class ChestScript : MonoBehaviour
    {
        [SerializeField]
        private Chest chest;
        private Animator animator;
        private bool isOpen = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        void Open()
        {


            animator.SetBool("open", true);
            chest.Open();
            isOpen = true;
            Book.Instance.Open();

        }

        void Close()
        {

            animator.SetBool("open", false);
            chest.Close();
            isOpen = false;
            Book.Instance.Close();

        }

        private void OnTriggerExit(Collider other)
        {
            if (isOpen)
            {
                Close();
            }
        }

        private void OnTriggerStay(Collider other)
        {
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen)
                {
                    Open();
                }
                else
                {
                    Close();
                }

            }
        }
    }
}

