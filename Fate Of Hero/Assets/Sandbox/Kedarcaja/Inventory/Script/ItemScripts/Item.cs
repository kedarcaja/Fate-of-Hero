using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string ItemName { get; set; }

    public string Description { get; set; }

    public ItemType ItemType { get; set; }

    public Quality Quality { get; set; }

    public string SpriteNeutral { get; set; }

    public string SpriteHighlighted { get; set; }

    public int MaxSize { get; set; }

    public int BuyPrice { get; set; }

    public int SellPrice { get; set; }

    public Item()
    {
        
    }

    public Item(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int buyPrice, int sellPrice)
    {
        this.ItemName = itemName;
        this.Description = description;
        this.ItemType = itemType;  
        this.Quality = quality;
        this.SpriteNeutral = spriteNeutral;
        this.SpriteHighlighted = spriteHighlighted;
        this.MaxSize = maxSize;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
    }

   
    public abstract void Use(Slot slot,ItemScript item);

    public virtual string GetTooltip(Inventory inv)
    {

        string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (Description != string.Empty)
        {
            newLine = "\n";
        }

        switch (Quality)
        {
            case Quality.COMON:
                color = "white";

                break;
            case Quality.UNCOMAN:
                color = "teal";

                break;
            case Quality.RARE:
                color = "navy";
                break;
            case Quality.EPIC:
                color = "magenta";
                break;
            case Quality.LEGENDARY:
                color = "orange";
                break;
            case Quality.ARTEFACT:
                color = "red";
                break;

        }
        return string.Format("<b><color=" + color + "><size=30>{0}</size></color></b><size=22><i><color=lime>" + newLine + "{1}</color></i>\n{2}\n</size>", ItemName, Description,ItemType.ToString().ToLower());

    }
}
