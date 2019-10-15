﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class ChestScript : MonoBehaviour
    {
     
        private Animator animator;
        private bool isOpen = false;

        public Bag Bag { get; private set; }


        private void Awake()
        {
            animator = GetComponent<Animator>();
            Bag = GetComponent<Bag>();

        }
        void Open()
        {
            if (animator)
                animator.SetBool("open", true);
            InventoryManager.Instance.Chest.Open(this);
            Book.Instance.Open();
            isOpen = true;

        }

        void Close()
        {
            if (animator)
                animator.SetBool("open", false);
            InventoryManager.Instance.Chest.Close(this);
            Book.Instance.Close();
            isOpen = false;

        }

        private void OnTriggerExit(Collider other)
        {
            if (isOpen && other.transform.root.name == "lootTrigger")
            {
                Close();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E) && other.name == "lootTrigger")
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

