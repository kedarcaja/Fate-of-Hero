using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorInvetory : ChestInventory {

    private static VendorInvetory instance;

    public static VendorInvetory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<VendorInvetory>();
            }
            return instance;
        }
    }

    protected override void Start()
    {
        EmptySlot = slots; 
        base.Start();
        GiveItem("Health potion");
        GiveItem("Rare Sword");
        GiveItem("Uncoman Head");
        GiveItem("Iron Ingot");
        GiveItem("Ruka smrti");
    }

    protected void GiveItem(string itemName)
    {
        GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

        tmp.AddComponent<ItemScript>();

        ItemScript newItem = tmp.GetComponent<ItemScript>();

        if (InventoryManager.Instance.ItemContainer.Consumeables.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables.Find(x => x.ItemName == itemName);
        }
        else if (InventoryManager.Instance.ItemContainer.Weapons.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.ItemName == itemName);
        }
        else if (InventoryManager.Instance.ItemContainer.Equipment.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Equipment.Find(x => x.ItemName == itemName);
        }
        else if (InventoryManager.Instance.ItemContainer.Materials.Exists(x => x.ItemName == itemName))
        {
            newItem.Item = InventoryManager.Instance.ItemContainer.Materials.Find(x => x.ItemName == itemName);
        }
        if (newItem != null)
        {
            AddItem(newItem);
        }
        Destroy(tmp);
    }

    public override void MoveItem(GameObject clicked)
    {
        
    }
}
