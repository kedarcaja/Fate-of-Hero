using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public float AttackSpeed { get; set; }
   

    public Weapon()
    {
    }

    public Weapon(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int buyPrice, int sellPrice, int strength, int agility, int intellect, int stamina, float attackSpeed, int minDamage, int maxDamage) :base(itemName,description,itemType,quality,spriteNeutral,spriteHighlighted,maxSize,buyPrice,sellPrice,strength,agility,intellect,stamina,minDamage,maxDamage)
    {
        this.AttackSpeed = attackSpeed;
       
    }
    public override void Use(Slot slot, ItemScript item)
    {
      //  Debug.Log("Equip " + ItemName);
        CharacterPanel.Instance.EquipItem(slot, item);
    }

    public override string GetTooltip(Inventory inv)
    {
        string stats = string.Empty;
        if (MinDamage > 0)
        {
            stats += "\nPoškození: "+ MinDamage.ToString()+"-"+ MaxDamage.ToString();
        }
        string equipmentTip = base.GetTooltip(inv);

        if (inv is VendorInvetory)
        {
            return string.Format("{0}\n<b><color=red><size=30>{2}</size></color></b> \n<size=22> AttackSpeed: {1}\n <color=yellow>Price: {3}</color></size>", equipmentTip, AttackSpeed, stats, SellPrice);
        }
        else if (VendorInvetory.Instance.IsOpen)
        {
            return string.Format("{0}\n<b><color=red><size=30>{2}</size></color></b> \n<size=22> AttackSpeed: {1}\n <color=yellow>Price: {3}</color></size>", equipmentTip, AttackSpeed, stats, BuyPrice);
        }
        else
        {
            return string.Format("{0}\n<b><color=red><size=30>{2}</size></color></b>\n<size=22> AttackSpeed: {1}</size>", equipmentTip, AttackSpeed, stats);
        }
        
    }
}
