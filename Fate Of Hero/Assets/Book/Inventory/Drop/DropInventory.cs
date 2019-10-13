using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace InventorySystem
{
    public class DropInventory : MonoBehaviour
    {
        [SerializeField]
        private Inventory droptory;
        private List<GameObject> physicalBags;

        [SerializeField]
        private GameObject sack;
        [SerializeField]
        private List<BagSaver> sackSavers = new List<BagSaver>();


        public Inventory Droptory => droptory;

        public Bag DropBagForItem()
        {
            return null;
        }
        public void DestroyDrop(GameObject sack)
        {

        }
        public void DestroyDrop(Bag bag)
        {

        }

        public void CreateDrop(Slot slot)
        {
            CreateDrop(slot.Peek(), slot.ItemCount);
        }
        public void CreateDrop(Item item, int count)
        {
            GameObject g = Instantiate(sack); // add position
            physicalBags.Add(g);
           
            Bag b = g.GetComponent<Bag>();
            b.Saver = sackSavers[0];
            droptory.Bags.Add(b);
            b.Add(item, count);
        }

    }
}
