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

        public List<Bag> Bags => bags;


        #endregion
        #region Unity Methods 


        #endregion
        #region  Collection Methods
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                
                Clear();
            }
        }
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
