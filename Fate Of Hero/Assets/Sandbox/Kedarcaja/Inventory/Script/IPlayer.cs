using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPlayer : MonoBehaviour {

    private static IPlayer instance;
    public float speed = 5f;
    public Inventory inventory;
    public Inventory CharPanel;
    public Inventory CharPanelII;
    private Inventory Chest;
    public Text statsText;

    [SerializeField]
    public Text goldText;
    public ItemScript[] items = new ItemScript[10];

    public int baseStrenght;
    public int baseAgility;
    public int baseIntellect;
    public int baseStamina;
    public int baseMinDamage;
    public int baseMaxDamage;

    private int strenght;
    private int agility;
    private int intellect;
    private int stamina;
    private int maxDamage;
    private int minDamage;
    private int gold;
    public int Gold
    {
        get
        {
            return gold;
        }

        set
        {
            goldText.text = "Gold: " + value;
            gold = value;
        }
    }
    public static IPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<IPlayer>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    void Start () {
        Gold = 0;
        SetStats(0, 0, 0, 0,0,0);
	}	
	void Update ()
    {
        transform.Translate(Input.GetAxis("Horizontal")*speed * UnityEngine.Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * UnityEngine.Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.Open();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Chest!=null)
            {
                if (Chest.canvasGroup.alpha == 0 || Chest.canvasGroup.alpha == 1)
                {
                    Chest.Open();
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (CharPanel != null)
            {
                CharPanel.Open();
            }
        }       
    }
    public void Click()
    {
        if (CharPanelII != null)
        {
            CharPanelII.Open();
        }
    }
    public void RandomItem()
    {
        int randomType = UnityEngine.Random.Range(0, 4);
        GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

        int randomItem;
        tmp.AddComponent<ItemScript>();
        ItemScript newItem = tmp.GetComponent<ItemScript>();

        switch (randomType)
        {

            case 0:
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
                newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
                break;
            case 1:
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
                newItem.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];
                break;
            case 2:
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
                newItem.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
                break;
            case 3:
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Materials.Count);
                newItem.Item = InventoryManager.Instance.ItemContainer.Materials[randomItem];
                break;
        }
        inventory.AddItem(newItem);
        Destroy(tmp);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Item")
        {
            RandomItem();

        }
        if (other.tag == "Chest" || other.tag == "Vendor")
        {
            Chest = other.GetComponent<InventoryLink>().LinkedInventory;
        }
        if (other.tag == "CraftingBench")
        {
            Chest = other.GetComponent<CraftingBenchScript>().craftingBench;
        }
        if (other.tag == "Material")
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
                    tmp.AddComponent<ItemScript>();
                    ItemScript newMaterial = tmp.GetComponent<ItemScript>();

                    newMaterial.Item = InventoryManager.Instance.ItemContainer.Materials[j];
                    inventory.AddItem(newMaterial);
                    Destroy(tmp);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chest" || other.tag == "CraftingBench" || other.tag == "Vendor")
        {
            if (Chest.IsOpen)
            {
                Chest.Open();
            }
           // Chest = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (inventory.AddItem(collision.gameObject.GetComponent<ItemScript>()))
            {
                Destroy(collision.gameObject);
            }
            
            
        }
    }
    public void SetStats(int strenght , int agility, int intellect, int stamina, int minDamage, int maxDamage)
    {
        this.strenght = strenght + baseStrenght;
        this.agility = agility + baseAgility;
        this.intellect = intellect + baseIntellect;
        this.stamina = stamina + baseStamina;
        this.minDamage = minDamage + baseMinDamage;
        this.maxDamage = maxDamage + baseMaxDamage;

        statsText.text = string.Format(" Damage: {4}-{5}\n Strenght: {0}\n Agility: {1}\n Intellect: {2}\n Stamina: {3}\n", this.strenght, this.agility, this.intellect, this.stamina,this.minDamage,this.maxDamage);

    }
}
