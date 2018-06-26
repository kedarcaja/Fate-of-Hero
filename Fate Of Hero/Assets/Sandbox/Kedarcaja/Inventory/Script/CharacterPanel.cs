using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : Inventory
{ 
    public Slot[] equipmentSlots;
    private static CharacterPanel instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }
            return CharacterPanel.instance;
        }
        set
        {
            instance = value;
        }
    }
    public Slot WeaponSlot
    {
        get { return equipmentSlots[9]; }
    }
    public Slot OffHandSlot
    {
        get { return equipmentSlots[10]; }
    }
    public override void CreateBackgroundLayout()
    {
        
    }
    public override void CreateLayout()
    {
        
    }
    private void Awake()
    {
        equipmentSlots = transform.GetComponentsInChildren<Slot>();
    }
    public void EquipItem(Slot slot, ItemScript item)
    {
        if (item.Item.ItemType == ItemType.MAINHAND || item.Item.ItemType == ItemType.TWOHAND && OffHandSlot.IsEmpty)
        {
            Slot.SwapItems(slot, WeaponSlot);
        } 
        else
        {
            Slot.SwapItems(slot, Array.Find(equipmentSlots, x => x.canContain == item.Item.ItemType));
        }        
    }
    public override void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();
        if (slot.GetComponentInParent<Inventory>().IsOpen && !tmpSlot.IsEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            InventoryManager.Instance.visialTextObject.text = tmpSlot.CurrentItem.GetTooltip(this);
            InventoryManager.Instance.sizeTextObject.text = InventoryManager.Instance.visialTextObject.text;
            InventoryManager.Instance.tooltipObject.SetActive(true);
            float xPos = slot.transform.position.x + 100 + slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop+50;

            InventoryManager.Instance.tooltipObject.transform.position = new Vector3(xPos, yPos);
        }
    }
    public void CalcStats()
    {
        int strenght = 0;
        int intellect = 0;
        int agility = 0;
        int stamina = 0;
        int minDamage = 0;
        int maxDamage = 0;

        foreach (Slot slot in equipmentSlots)
        {
            if (!slot.IsEmpty)
            {
                Equipment e = (Equipment)slot.CurrentItem.Item;
                //Weapon w = (Weapon)slot.CurrentItem.Item;
                strenght += e.Strength;
                agility += e.Agility;
                intellect += e.Intellect;
                stamina += e.Stamina;
                minDamage += e.MinDamage;
                maxDamage += e.MaxDamage;
                
            }
        }
        IPlayer.Instance.SetStats(strenght, agility, intellect, stamina, minDamage,maxDamage);
    }
    public override void SaveInventory()
    {
        string conntent = string.Empty;

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (!equipmentSlots[i].IsEmpty)
            {
                conntent += i + "-" + equipmentSlots[i].Items.Peek().Item.ItemName + ";";
            }
        }
        PlayerPrefs.SetString("CharPanel", conntent);
        PlayerPrefs.Save();
    }
    public override void LoadInventory()
    {
        foreach (Slot slot in equipmentSlots)
        {
            slot.ClearSlot();
        }
        string content = PlayerPrefs.GetString("CharPanel");
        string[] splitContent = content.Split(';');

        for (int i = 0; i < splitContent.Length - 1; i++)
        {
            string[] splitValues = splitContent[i].Split('-');
            int index = Int32.Parse(splitValues[0]);
            string itemName = splitValues[1];

            GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);
            loadedItem.AddComponent<ItemScript>();

            if (index == 9 || index == 10)
            {
                loadedItem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.ItemName == itemName);
            }
            else
            {
                loadedItem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemContainer.Equipment.Find(x => x.ItemName == itemName);
            }

            equipmentSlots[index].AddItem(loadedItem.GetComponent<ItemScript>());
            Destroy(loadedItem);
            CalcStats();
        }
    }
    }
