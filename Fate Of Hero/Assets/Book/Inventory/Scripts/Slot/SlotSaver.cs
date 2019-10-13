using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    [CreateAssetMenu(menuName = "Inventory/SlotSaver")]
    public class SlotSaver : ScriptableObject
    {

        [SerializeField]
        private List<Item> items = new List<Item>();

        [SerializeField]
        private List<ItemType> canContain = new List<ItemType>();

        [SerializeField]
        private bool isGeneric = false; // can contain all => ignores canContain

        [SerializeField]
        private Sprite emptySprite;
        [SerializeField]
        private bool swapBackgroundOnFill = true;

        public List<Item> Items => items;
        public List<ItemType> CanContain => canContain;
        public bool IsGeneric => isGeneric;

        public Sprite EmptySprite => emptySprite == null ? InventoryManager.Instance.DefaultEmptySlotSprite : emptySprite;

        public bool SwapBackgroundOnFill  => swapBackgroundOnFill; 
    }
}