using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingBench : Inventory
{
    private static CraftingBench instance;
    public GameObject prefabButton;
    public GameObject previewSlot;
    public GameObject previewSlotBackground;
    private Dictionary<string, Item> craftingItems = new Dictionary<string, Item>();

    public static CraftingBench Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CraftingBench>();
            }
            return instance;
        }

       
    }

    public override void CreateLayout()
    {
        base.CreateLayout();
        GameObject craftBtn;
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight + slotSize + slotPaddingTop * 2);

        if (!GameObject.Find("CraftButton"))
        {
            craftBtn = Instantiate(prefabButton);

            RectTransform btnRect = craftBtn.GetComponent<RectTransform>();
            craftBtn.name = "CraftButton";
            craftBtn.transform.SetParent(SlotParent.transform);
            btnRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft - 2, -slotPaddingTop * 4 - (slotSize * 3));
            btnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((slotSize * 2) + (slotPaddingLeft * 2) - 5) * InventoryManager.Instance.canvas.scaleFactor);

            btnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
            craftBtn.transform.SetParent(transform);

            craftBtn.GetComponent<Button>().onClick.AddListener(CraftItem);
        }

        if (!GameObject.Find("PreviewSlot"))
        {
            previewSlot = Instantiate(InventoryManager.Instance.slotPrefab);
            RectTransform slotRect = previewSlot.GetComponent<RectTransform>();
            previewSlot.name = "PreviewSlot";
            previewSlot.transform.SetParent(SlotParent.transform);


            slotRect.localPosition = inventoryRect.localPosition + new Vector3((slotPaddingLeft * 3) + (slotSize * 2), -slotPaddingTop * 4 - (slotSize * 4) + 22);

            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
            previewSlot.transform.SetParent(SlotParent.transform);
            previewSlot.GetComponent<Slot>().ClickAble = false;
        }

    }
    public override void CreateBackgroundLayout()
    {
        base.CreateBackgroundLayout();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight + slotSize + slotPaddingTop * 2);

        if (!GameObject.Find("PreviewSlotBG"))
        {
            previewSlotBackground = Instantiate(InventoryManager.Instance.BackgroundSlotPrefab);
            RectTransform slotRect = previewSlotBackground.GetComponent<RectTransform>();
            previewSlotBackground.name = "PreviewSlotBG";
            previewSlotBackground.transform.SetParent(BackgroundParent.transform);

            slotRect.localPosition = inventoryRect.localPosition + new Vector3((slotPaddingLeft * 3) + (slotSize * 2), -slotPaddingTop * 4 - (slotSize * 4) + 48);

            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
            previewSlotBackground.transform.SetParent(BackgroundParent.transform);

        }
           

    }
    public void CreateBluePrints()
    {
        craftingItems.Add("EMPTY-Iron Ingot-EMPTY-EMPTY-Iron Ingot-EMPTY-EMPTY-Wood Log-EMPTY-", InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.ItemName == "Rare Sword"));

        craftingItems.Add("EMPTY-EMPTY-EMPTY-Stone-Stone-Stone-Stone-EMPTY-Stone-", InventoryManager.Instance.ItemContainer.Equipment.Find(x => x.ItemName == "Uncoman Head"));
    }
    public void CraftItem()
    {
        string output = string.Empty;

        foreach (GameObject slot in allSlots)
        {
            Slot tmp = slot.GetComponent<Slot>();

            if (tmp.IsEmpty)
            {
                output += "EMPTY-";
            }
            else
            {
                output += tmp.CurrentItem.Item.ItemName + "-";
            }
        }
        if (craftingItems.ContainsKey(output))
        {
            GameObject tmpObj = Instantiate(InventoryManager.Instance.itemObject);

            tmpObj.AddComponent<ItemScript>();

            ItemScript cractedItem = tmpObj.GetComponent<ItemScript>();

            Item tmpItem;

            craftingItems.TryGetValue(output, out tmpItem);

            if (tmpItem != null)
            {
                cractedItem.Item = tmpItem;
                
                if (IPlayer.Instance.inventory.AddItem(cractedItem))
                {
                    foreach (GameObject Slot in allSlots)
                    {
                        Slot.GetComponent<Slot>().RemoveItem();
                    }
                }
                Destroy(tmpObj);
            }
        }
        Debug.Log(output);
        UpdatePreview();
    }
    public override void MoveItem(GameObject clicked)
    {
        base.MoveItem(clicked);
        UpdatePreview();
    }
    public void UpdatePreview()
    {
        string output = string.Empty;
        previewSlot.GetComponent<Slot>().ClearSlot();
        foreach (GameObject slot in allSlots)
        {
            Slot tmp = slot.GetComponent<Slot>();

            if (tmp.IsEmpty)
            {
                output += "EMPTY-";
            }
            else
            {
                output += tmp.CurrentItem.Item.ItemName + "-";
            }
        }
        if (craftingItems.ContainsKey(output))
        {
            GameObject tmpObj = Instantiate(InventoryManager.Instance.itemObject);

            tmpObj.AddComponent<ItemScript>();

            ItemScript cractedItem = tmpObj.GetComponent<ItemScript>();

            Item tmpItem;

            craftingItems.TryGetValue(output, out tmpItem);

            if (tmpItem != null)
            {
                cractedItem.Item = tmpItem;

                previewSlot.GetComponent<Slot>().AddItem(cractedItem);
                Destroy(tmpObj);

            }
        }
       
    }
    public override void LoadInventory()
    {
        base.LoadInventory();
        UpdatePreview();
    }
    public override void Open()
    {
        base.Open();

        foreach (GameObject slot in allSlots)
        {
            Slot tmpSlot = slot.GetComponent<Slot>();
            int count = tmpSlot.Items.Count;

            for (int i = 0; i < count; i++)
            {
                ItemScript tmpItem = tmpSlot.RemoveItem();

                if (!IPlayer.Instance.inventory.AddItem(tmpItem))
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                    v *= 1;
                    GameObject tmpDrp = Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);
                    tmpDrp.AddComponent<ItemScript>();
                    tmpDrp.GetComponent<ItemScript>().Item = tmpItem.Item;
                }
                
            }
        }
    }
}
