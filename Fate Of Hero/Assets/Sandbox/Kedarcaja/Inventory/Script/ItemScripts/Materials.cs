using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Item
{
    public Materials(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int buyPrice, int sellPrice) : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize,buyPrice,sellPrice)
    {
    }

    public Materials()
    {
    }

    public override string GetTooltip(Inventory inv)
    {
      string materialTip = base.GetTooltip(inv);
        if (inv is VendorInvetory)
        {
            return string.Format("{0} \n<size=22><color=yellow>Price: {1}</color></size>", materialTip, BuyPrice);
        }
        else if (VendorInvetory.Instance.IsOpen)
        {
            return string.Format("{0} \n<size=22><color=yellow>Price: {1}</color></size>", materialTip, SellPrice);
        }
        return materialTip;
    }
    public override void Use(Slot slot, ItemScript item)
    {
       
    }
}
