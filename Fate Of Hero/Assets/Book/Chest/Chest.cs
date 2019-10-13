using System.Collections;
using System.Collections.Generic;
using Unity.Extensions;
using UnityEngine;

namespace InventorySystem
{
    public class Chest : MonoBehaviour
    {
      
        private Bag bag;

        CanvasGroup group;

        private void Awake()
        {
            group = GetComponentInParent<CanvasGroup>();
            bag = GetComponent<Bag>();

        }

        public void Open()
        {
            group.Active(true);
            CharacterPanel.Instance.GetComponent<CanvasGroup>().Deactive(true);
        }

        public void Close()
        {
            group.Deactive(true);
            CharacterPanel.Instance.GetComponent<CanvasGroup>().Active(true);
        }
    }
}

