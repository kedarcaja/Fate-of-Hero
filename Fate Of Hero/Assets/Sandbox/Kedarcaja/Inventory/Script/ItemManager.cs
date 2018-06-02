using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public enum Category {EQUIPMENT,WEAPON, CONSUMEABLE, MATERIALS }
public class ItemManager : MonoBehaviour {

    public string itemName;

    [TextArea]
    public string description;

    public ItemType itemType;

    public Quality quality;

    public Category category;

    public string spriteNeutral;

    public string spriteHighlghted;

    public int maxSize;

    public int strength;

    public int agility;

    public int intellect;

    public int stamina;

    public float attackSpeed;

    public int health;

    public int mana;

    public int minDamage;

    public int maxDamage;

    public int buyPrice;

    public int sellPrice;


    public void CreateItem() {

        ItemContainer itemContainer = new ItemContainer();

        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable), typeof(Materials) };

        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        serializer.Serialize(fs, itemContainer);

        fs.Close();

        switch (category)
        {
            case Category.EQUIPMENT:
                itemContainer.Equipment.Add(new Equipment(itemName, description, itemType, quality, spriteNeutral, spriteHighlghted, maxSize, buyPrice, sellPrice, strength, agility, intellect, stamina));
                break;
            case Category.WEAPON:
                itemContainer.Weapons.Add(new Weapon(itemName, description, itemType, quality, spriteNeutral, spriteHighlghted, maxSize, buyPrice, sellPrice, strength, agility, intellect, stamina, attackSpeed,minDamage,maxDamage));
                break;
            case Category.CONSUMEABLE:
                itemContainer.Consumeables.Add(new Consumeable(itemName, description, itemType, quality, spriteNeutral, spriteHighlghted, maxSize, buyPrice, sellPrice , health, mana));
                break;
            case Category.MATERIALS:
                itemContainer.Materials.Add(new Materials(itemName, description, itemType, quality, spriteNeutral, spriteHighlghted, maxSize, buyPrice, sellPrice));
                break;
        }
        fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Create);
        serializer.Serialize(fs, itemContainer);
        fs.Close();
        Debug.Log("<color=Green>Item: "+ itemName +" byl přidán do XML Databáze </color>");
    }
}

