using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intellect { get; set; }
    public int Stamina { get; set; }

    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }



    public Equipment()
    {
    }

    public Equipment(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int buyPrice, int sellPrice, int strength, int agility, int intellect,int stamina, int minDamage, int maxDamage) : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, buyPrice, sellPrice)
    {
        this.Strength = strength;
        this.Agility = agility;
        this.Intellect = intellect;
        this.Stamina = stamina;
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;

    }



    public override void Use(Slot slot, ItemScript item)
    {
      //  Debug.Log("Equip " + ItemName);
        CharacterPanel.Instance.EquipItem(slot, item);
    }

    public override string GetTooltip(Inventory inv)
    {
        
        string stats = string.Empty;
        if (Strength > 0)
        {
            stats += "\n+" + Strength.ToString() + " Strength";
        }
        if (Agility > 0)
        {
            stats += "\n+" + Agility.ToString() + " Agility";
        }
        if (Intellect > 0)
        {
            stats += "\n+" + Intellect.ToString() + " Intellect";
        }        
        if (Stamina > 0)
        {
            stats += "\n+" + Stamina.ToString() + " Stamina";
        }
        if (MinDamage > 0)
        {
            stats += "\nPoškození: " + MinDamage.ToString() + "-" + MaxDamage.ToString();
        }
        string itemTip = base.GetTooltip(inv);
        if (inv is VendorInvetory && !(this is Weapon))
        {
            return string.Format("{0}" + "<size=22>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, BuyPrice);
        }
        else if (VendorInvetory.Instance.IsOpen && !(this is Weapon))
        {
            return string.Format("{0}" + "<size=22>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, SellPrice);
        }
        else
        {
            return string.Format("{0}" + "<size=22>{1}</size>", itemTip, stats);
        }
       
    }

}
