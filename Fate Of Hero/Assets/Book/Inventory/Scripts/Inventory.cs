using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour, IInvCollection
    {
        #region Variables
        [SerializeField]
        private List<Bag> bags = new List<Bag>();



        [SerializeField]
        private List<Item> testItems; // delete

        public List<Bag> Bags => bags;
        #endregion
        #region Unity Methods 

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                testItems.ForEach(i => Add(i));
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Clear();
            }
        }
        #endregion
        #region  Collection Methods

        public void Add(Item item)
        {
            if (BagForItem(item) != null)
            {
                BagForItem(item).Add(item);
            }

        }
        public void Add(Item item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(item);
            }
        }
        public bool CanContain(Item item)
        {
            return true;
        }

        public void Clear()
        {
            bags.ForEach(bags => bags.Clear());
        }

        public bool IsFull()
        {
            return bags.All(bags => bags.IsFull());
        }

        public bool IsEmpty()
        {
            return bags.All(bags => bags.IsEmpty());
        }
        public Bag BagForItem(Item item)
        {
            return bags.Find(bags => bags.CanContain(item));
        }
        public void SortBag(Bag bag)
        {
            bag.Sort();
        }
        public void SortCurrentBag()
        {
            SortBag(InventoryManager.Instance.GetCurrentlyOpenedBag());
        }
        #endregion
    }
}
