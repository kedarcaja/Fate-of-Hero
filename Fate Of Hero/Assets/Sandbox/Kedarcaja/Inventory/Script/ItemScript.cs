using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { CONSUMEABLE, MAINHAND, TWOHAND, OFFHAND, HEAD, SPAULDER, CHEST, BOOTS, LEGS, RING, TRINKET, BRACERS, BELT, EARRING, ARM, ARMGUARD,GENERIC,GENERICWEAPON,MATERIALS}
public enum Quality { COMON,UNCOMAN,RARE,EPIC,LEGENDARY,ARTEFACT }
public class ItemScript : MonoBehaviour {

    #region Variables
 

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    private Item item;

    public Item Item
    {
        get
        {
            return item;
        }

        set
        {
            item = value;
            spriteHighlighted = Resources.Load<Sprite>(value.SpriteHighlighted);
            spriteNeutral = Resources.Load<Sprite>(value.SpriteNeutral);
        }
    }

    #endregion

    #region Unity Metod

    public void Use(Slot slot) {
        item.Use(slot, this);
    }

    public string GetTooltip(Inventory inv)
    {
        return item.GetTooltip(inv);
    }

    #endregion
}

