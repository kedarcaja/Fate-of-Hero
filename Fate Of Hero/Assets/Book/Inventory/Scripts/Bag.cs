using System.Collections;
using System.Collections.Generic;
using System.Extensions;
using System.Linq;
using Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;
namespace InventorySystem
{
    public class Bag : MonoBehaviour, IInvCollection, IHiddable
    {
        #region  Variables

        private CanvasGroup canvasGroup;
        private GridLayoutGroup grid;

        [SerializeField]
        private List<Slot> slots = new List<Slot>();
        [SerializeField]
        private BagSaver saver;
        [SerializeField]
        private int maxBagLevel = 11;
        [SerializeField]
        private int slotsInRow = 11;

        public BagSaver Saver { get => saver; set => saver = value; }

        #endregion

        #region Unity Methods
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            grid = GetComponent<GridLayoutGroup>();
            if (grid != null)
                grid.constraintCount = slotsInRow;
            DrawLayout();
        }

        #endregion

        #region Drawing Methods
        private void DrawLayout()
        {
            if (!saver.StaticLayout)
            {
                RemoveSlotsFromTransform();
               
                for (int i = 0; i < saver.Level; i++)
                {
                    DrawRow();
                }
                for (int i = 0; i < slots.Count; i++)
                {
                   
                    slots[i].Saver = saver.Slots[i];
                }
            }
        }
        private void RemoveSlotsFromTransform()
        {
            slots.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform c = transform.GetChild(i);
                if (c.GetComponent<Slot>() != null)
                {
                    Destroy(c.gameObject);
                }
            }
        }
        private void DrawRow()
        {
            for (int i = 0; i <  slotsInRow; i++)
            {
                GameObject g = Instantiate(InventoryManager.Instance.SlotTemplate);
                g.transform.SetParent(this.transform);
                Slot s = g.GetComponent<Slot>();
                slots.Add(s);
            }
        }

        #endregion

        #region  Collection Methods

        public void Add(Item item)
        {
            if (!IsFull() && SlotForItem(item) != null)
            {
                SlotForItem(item).Add(item);
            }
            else
            {
                //TODO drop or return to start slot
                Debug.Log("<color=red>Item was dropped</color>");
            }
        }
        public void Add(Item item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(item);
            }
        }
        public Slot SlotForItem(Item item)
        {
            Slot x = slots.Find(s => !s.IsEmpty() && !s.IsFull() && s.Peek().EqualsInstanceProperties(item) && s.CanContain(item));
            if (x == null)
            {
                x = slots.Find(s => s.IsEmpty() && s.CanContain(item));
            }
            return x;
        }
        public Slot SlotForItemToSwap(Item item)
        {
            return slots.Find(s => !s.IsEmpty() && s.CanContain(item) && ((s.IsFull()) || s.Peek() != item));
        }

        public bool CanContain(Item item)
        {
            return saver.Category == item.MainType || saver.IsGeneric;
        }
        public void Clear()
        {
            slots.ForEach(s => s.Clear());
        }
        public bool IsEmpty()
        {
            return slots.All(s => s.IsEmpty());
        }
        public bool IsFull()
        {
            return slots.All(s => s.IsFull());
        }

        #region Sort methods

        public void Sort()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                MergeAndFill();
            }
            Quicksort(slots.ToArray(), 0, slots.Count);
        }
        private void MergeAndFill()
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Slot s = slots[i];

                if (SlotForItem(s.Peek()) != null && !s.IsEmpty())
                {
                    Slot sf = SlotForItem(s.Peek());
                    if (sf.IsEmpty())
                    {
                        sf.Add(s.Peek(), s.ItemCount);
                        s.Clear();
                    }
                    else
                    {
                        InventoryManager.Instance.MergeSlots(s, sf);
                    }
                }
            }
        }

        private void Quicksort(Slot[] array, int left, int right)
        {
            if (left < right)
            {
                int boundary = left;
                for (int i = left + 1; i < right; i++)
                {
                    if (CompareSlots(array[i], array[left]) == -1)
                    {
                        InventoryManager.Instance.SwapSlots(array[i], array[++boundary]);
                    }
                }
                InventoryManager.Instance.SwapSlots(array[left], array[boundary]);
                Quicksort(array, left, boundary);
                Quicksort(array, boundary + 1, right);
            }
        }

        /// <summary>
        /// Alphabetic comparesion of content of two slots
        /// </summary>
        /// <param name="slotA"></param>
        /// <param name="slotB"></param>
        /// <returns>   0 == same ||  1 == left is before right || -1 == right is before left</returns>
        private int CompareSlots(Slot slotA, Slot slotB)
        {
            if (slotA.IsEmpty() || slotB.IsEmpty()) return 1;

            return string.Compare(slotA.Peek().name, slotB.Peek().name);
        }

        #endregion


        #endregion

        #region Visible Handles
        public void Hide()
        {
            canvasGroup.Deactive(true);
        }

        public void Show()
        {
            canvasGroup.Active(true);
        }
        #endregion

        public void LevelUp()
        {
            if (saver.Level < maxBagLevel)
            {
                saver.Level++;
                if (!saver.StaticLayout)
                    DrawLayout();
            }
        }
    }

}

