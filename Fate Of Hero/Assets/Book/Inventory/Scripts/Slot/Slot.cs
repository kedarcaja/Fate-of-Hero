using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Extensions;
using System.Reflection;

namespace InventorySystem
{
    public class Slot : MonoBehaviour, IMouse, IInvCollection
    {
        #region Variables
        [SerializeField]
        private SlotSaver saver;
        private ItemContainer itemContainer;


        private Outline outline;
        private Image img;

        public int ItemCount => saver.Items.Count;
        public SlotSaver Saver { get => saver; set => saver = value; }
        public ItemContainer ItemContainer => GetComponentInChildren<ItemContainer>();

        #endregion

        #region Unity Methods
        void Awake()
        {
            itemContainer = transform.GetChild(0).GetComponent<ItemContainer>();
            outline = GetComponent<Outline>();
            img = GetComponent<Image>();
        }
        private void Start()
        {
            UpdateUI();  // loads current state of slot on start
        }
        #endregion

        #region  Collection Methods

        public void Add(Item item)
        {
            if (!IsFull() && CanContain(item))
            {
                saver.Items.Add(item);
            }
            UpdateUI(item);
        }

        public void Add(Item item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(item);
            }
        }
        public Item Peek()
        {
            return saver.Items.Count > 0 ? saver.Items.First() : null;
        }

        public void Remove()
        {
            if (!IsEmpty())
            {
                saver.Items.Remove(Peek());
            }
            UpdateUI();
        }
        public void Remove(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Remove();
            }
        }
        public void Clear()
        {
            saver.Items.Clear();
            UpdateUI();
        }
        public bool IsEmpty()
        {
            return Peek() == null;
        }

        public bool IsFull()
        {
            return saver.Items.Count > 0 && Peek().MaxInSlot == saver.Items.Count;
        }
        public bool CanContain(Item item)
        {
            return saver.IsGeneric || saver.CanContain.Contains(item.ItemType);
        }
        private string GetCountText()
        {
            return saver.Items.Count > 1 ? saver.Items.Count.ToString() : "";
        }
        #endregion

        #region Activation Handlers
        public void UpdateUI(Item item)
        {
            if (itemContainer == null)
                itemContainer = GetComponentInChildren<ItemContainer>();

            if (saver != null && item != null)
            {
                itemContainer.UpdateUI(item.Sprite, item.QualityBackground, GetCountText(), !IsEmpty());

                if (saver.SwapBackgroundOnFill)
                {
                    img.sprite = InventoryManager.Instance.DefaultEmptySlotSprite;
                }
            }
            else
            {
                if (saver != null && saver.SwapBackgroundOnFill)
                    img.sprite = saver.EmptySprite;

                Sprite s = InventoryManager.Instance.TransparentItemSlot;
                itemContainer.UpdateUI(s, s, "", false);
            }
        }
        public void UpdateUI()
        {
            UpdateUI(Peek());
        }

        #endregion

        #region  Pointer Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            if (InventoryManager.Instance.IsDraging)
            {
                InventoryManager.Instance.DropToEmptySlot(this);
            }

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsEmpty())
            {
                outline.enabled = true;
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            outline.enabled = false;
        }
        public void ShareVisual(Slot slot)
        {
            slot.saver = this.saver;
            slot.UpdateUI();
        }
        public void DragStart()
        {
            itemContainer.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        public void DragEnd()
        {
            itemContainer.GetComponent<Image>().color = Color.white;
        }

        public string GetItemStats(Slot other)
        {
            if (IsEmpty()) return "";

            string stats = "";
            bool comp = other != null && !other.IsEmpty();

            foreach (PropertyInfo p in Peek().GetType().GetProperties())
            {
                if (!p.Name.StartsWith("__")) continue;

                float valueA = float.Parse(Peek().GetPropertyValueByName(p.Name).ToString());

                float valueB = 0;
                string color = "#ffffff";
                string op = "";
                float diff = 0;
                if (comp)
                {
                    valueB = float.Parse(other.Peek().GetPropertyValueByName(p.Name).ToString());
                    diff = valueA - valueB;


                    if (valueA > valueB)
                    {
                        op = "+";
                        color = "green";
                    }
                    else if (valueA < valueB)
                    {
                       color = "red";
                    }
                }
                    valueA = (float)Math.Round(valueA,2);
                    valueB = (float)Math.Round(valueB,2);
                string nam = p.Name.Replace("__", "");
                string cp =  $"<color={color}> {op}{(diff == 0 ? string.Empty : diff.ToString())}</color>";
                
                stats += string.Format($"\n{nam}: {valueA} {(comp ? cp : string.Empty)}");
            }
            return stats;
        }

        #endregion
    }
}



