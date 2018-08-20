using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumeable : Item
{
   
   public int Health { get; set; }
   
    public int Mana { get; set; }
  

    public Consumeable()
    {
    }

    public Consumeable(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int buyPrice, int sellPrice,int health, int mana) : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize,buyPrice,sellPrice)
    {
        this.Health = health;
        this.Mana = mana;

    }

    public override void Use(Slot slot, ItemScript item)
    {
        Debug.Log("Used " + ItemName);
        slot.RemoveItem();
    }

    public override string GetTooltip(Inventory inv)
    {
        string stats = string.Empty;
        if (Health > 0)
        {
            stats += "\nRestores " + Health.ToString() + " Health";
        }
        if (Mana > 0)
        {
            stats += "\nRestores " + Mana.ToString() + " Mana";
        }
        string itemTip = base.GetTooltip(inv);

        if (inv is VendorInvetory)
        {
            return string.Format("{0}" + "<size=22>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, SellPrice);
        }
        else if (VendorInvetory.Instance.IsOpen)
        {
            return string.Format("{0}" + "<size=22>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, BuyPrice);
        }
        else
        {
            return string.Format("{0}" + "<size=22>{1}</size>", itemTip, stats);
        }

        
    }
}
