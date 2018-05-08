using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }
            return instance;
        }

    }
    public GameObject slotPrefab;
    public GameObject BackgroundSlotPrefab;
    public GameObject IconPrefab;
    private GameObject hoverObject;
    public GameObject HoverObject
    {
        get
        {
            return hoverObject;
        }

        set
        {
            hoverObject = value;
        }
    }
    public GameObject dropItem;
    public GameObject itemObject;
    public GameObject tooltipObject;
    public Text sizeTextObject;
    public Text visialTextObject;
    public Canvas canvas;
    private Slot from;
    public Slot From
    {
        get
        {
            return from;
        }

        set
        {
            from = value;
        }
    }
    private Slot to;
    public Slot To
    {
        get
        {
            return to;
        }

        set
        {
            to = value;
        }
    }
    private GameObject clicked;
    public GameObject Clicked
    {
        get
        {
            return clicked;
        }

        set
        {
            clicked = value;
        }
    }
    public Text StackText;
    public GameObject selectStackSize;
    public EventSystem eventSystem;
    private int spliteAmount;
    public int SpliteAmount
    {
        get
        {
            return spliteAmount;
        }

        set
        {
            spliteAmount = value;
        }
    }
    private int maxStackCount;
    public int MaxStackCount
    {
        get
        {
            return maxStackCount;
        }

        set
        {
            maxStackCount = value;
        }
    }
    public Slot MovingSlot
    {
        get
        {
            return movingSlot;
        }

        set
        {
            movingSlot = value;
        }
    }
    public ItemContainer ItemContainer
    {
        get
        {
            return itemContainer;
        }

        set
        {
            itemContainer = value;
        }
    }
    private Slot movingSlot;
    private ItemContainer itemContainer = new ItemContainer();
   


    void Awake () {
        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable), typeof(Materials) };
        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Items.xml");
        ItemContainer = (ItemContainer)serializer.Deserialize(textReader);
        textReader.Close();
        CraftingBench.Instance.CreateBluePrints();
	}
	
	void Update () {
		
	}

    public void SetStackInfo(int MaxStackCount) //Nastavení informací o stacku
    {
        selectStackSize.SetActive(true);
        tooltipObject.SetActive(false);
        SpliteAmount = 0;
        this.MaxStackCount = MaxStackCount;
        StackText.text = SpliteAmount.ToString();
    }

    public void Save()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().SaveInventory();
        }
        foreach (GameObject chest in chests)
        {
            chest.GetComponent<InventoryLink>().SaveInventory();
        }
    }
    public void Load()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().LoadInventory();
        }
        foreach (GameObject chest in chests)
        {
            chest.GetComponent<InventoryLink>().LoadInventory();
        }
    }
  
}
