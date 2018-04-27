using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLink : MonoBehaviour {

    #region Variables
    public ChestInventory LinkedInventory;
    public int slots, rows;
    private List<Stack<ItemScript>> allSlots;
    private bool active = false;
    #endregion

    #region Unity Metod
    private void Start()
    {
        allSlots = new List<Stack<ItemScript>>(slots);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (LinkedInventory.FadingOut)
            {
                LinkedInventory.instantClose = true;
                LinkedInventory.MoveItemsToChest();
            }
            active = true;
            LinkedInventory.UpdateBackgroundLayout(rows, slots);
            LinkedInventory.UpdateLayout(allSlots,rows, slots);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            active = false;
        }
    }

    public void SaveInventory()
    {
        string content = string.Empty;
        for (int i = 0; i < allSlots.Count; i++)
        {
            if (allSlots[i] != null && allSlots[i].Count > 0)
            {
                content += i + "-" + allSlots[i].Peek().Item.ItemName  + "-" + allSlots[i].Count.ToString() + ";";
            } 
        }

        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.Save();
    }
    public virtual void LoadInventory()
    {
        string content = PlayerPrefs.GetString(gameObject.name + "content");
        allSlots = new List<Stack<ItemScript>>();

        for (int i = 0; i < slots; i++)
        {
            allSlots.Add(new Stack<ItemScript>());
        }
        if (content != string.Empty)
        {
        string[] splitContent = content.Split(';'); // 0-MANA-3

            for (int x = 0; x < splitContent.Length - 1; x++)
            {
            string[] splitValues = splitContent[x].Split('-');

            int index = Int32.Parse(splitValues[0]); //0

            string itemName = splitValues[1];//MANA

            int amount = Int32.Parse(splitValues[2]); //"3"

            Item tmp = null;
            for (int i = 0; i < amount; i++)
            {
                GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);
                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemContainer.Consumeables.Find(item => item.ItemName == itemName);
                }
                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemContainer.Equipment.Find(item => item.ItemName == itemName);
                }
                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemContainer.Weapons.Find(item => item.ItemName == itemName);
                }
                if (tmp == null)
                {
                    tmp = InventoryManager.Instance.ItemContainer.Materials.Find(item => item.ItemName == itemName);
                }
                loadedItem.AddComponent<ItemScript>();
                loadedItem.GetComponent<ItemScript>().Item = tmp;
                allSlots[index].Push(loadedItem.GetComponent<ItemScript>());

                Destroy(loadedItem);
            }
        }

        }
        if (active)
        {
            LinkedInventory.UpdateBackgroundLayout(rows, slots);
            LinkedInventory.UpdateLayout(allSlots, rows, slots);
        }
    }
    #endregion
}
