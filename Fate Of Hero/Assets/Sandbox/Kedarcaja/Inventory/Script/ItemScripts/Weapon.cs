using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public float AttackSpeed { get; set; }
    public Weapon()
    {
    }

    public Weapon(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int buyPrice, int sellPrice, int strength, int agility, int intellect, int stamina,float attackSpeed) : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, strength, agility, intellect, stamina, buyPrice,sellPrice )
    {
        this.AttackSpeed = attackSpeed;
    }
    public override void Use(Slot slot, ItemScript item)
    {
        Debug.Log("Equip " + ItemName);
        CharacterPanel.Instance.EquipItem(slot, item);
    }

    public override string GetTooltip(Inventory inv)
    {
       
        string equipmentTip = base.GetTooltip(inv);

        if (inv is VendorInvetory)
        {
            return string.Format("{0} \n <size=22> AttackSpeed: {1}\n<color=yellow>Price: {2}</color></size>", equipmentTip, AttackSpeed,BuyPrice);
        }
        else if (VendorInvetory.Instance.IsOpen)
        {
            return string.Format("{0} \n <size=22> AttackSpeed: {1}\n<color=yellow>Price: {2}</color></size>", equipmentTip, AttackSpeed, SellPrice);
        }
        else
        {
            return string.Format("{0} \n <size=22> AttackSpeed: {1}</size>", equipmentTip, AttackSpeed);
        }
        
    }
}
