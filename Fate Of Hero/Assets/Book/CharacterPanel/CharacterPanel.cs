using System.Collections;
using System.Collections.Generic;
using System.Extensions;
using UnityEngine;

namespace InventorySystem
{
    [RequireComponent(typeof(Inventory))]
    public class CharacterPanel : MonoBehaviour
    {
        private Inventory equipment;
        public static CharacterPanel Instance { get; private set; }

        public Inventory Equipment => equipment;

        void Awake()
        {
            Instance = GetComponent<CharacterPanel>();
            equipment = GetComponent<Inventory>();
        }
        public void Equip(Slot from)
        {
            //TODO  condirional equiping -> shield and sword -> dual hand sword without shield

            if (CanEquip(from))
            {
                Slot to = equipment.BagForItem(from.Peek()).SlotForItem(from.Peek());

                if (to == null)
                {
                    to = equipment.BagForItem(from.Peek()).SlotForItemToSwap(from.Peek());
                }


                if (to.IsEmpty())
                {
                    to.Add(from.Peek(), from.ItemCount);
                    from.Clear();
                }

                else if (!to.IsEmpty() && !to.IsFull() && from.Peek().EqualsInstanceProperties(to.Peek()))
                {
                    InventoryManager.Instance.MergeSlots(from, to);
                }

                else
                {
                    InventoryManager.Instance.SwapSlots(from, to);
                }

            }
        }
        private bool CanEquip(Slot from)
        {
            return from.Peek() is Equipment && (from.Peek() as Equipment).Equipable;
        }

    }

}