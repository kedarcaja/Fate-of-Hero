using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestInventory : Inventory
{
    private List<Stack<ItemScript>> chestItems;
    private int chestSlot;
    int draw;

    public override void CreateBackgroundLayout()
    {
        allBackgroundSlot = new List<GameObject>();

        for (int i = 0; i < slots; i++)
        {
            GameObject newBackgroundSlot = Instantiate(InventoryManager.Instance.BackgroundSlotPrefab);
            newBackgroundSlot.name = "Slot Background";
            newBackgroundSlot.transform.SetParent(BackgroundParent.transform);
            allBackgroundSlot.Add(newBackgroundSlot);
            newBackgroundSlot.SetActive(false);
            draw++;
        }
        if (draw== slots)
        {
            CreateLayout();
        }
    }
    public override void CreateLayout()
    {
        allSlots = new List<GameObject>();

        for (int i = 0; i < slots; i++)
        {
            GameObject newSlot = Instantiate(InventoryManager.Instance.slotPrefab);
            newSlot.name = "Slot";
            newSlot.transform.SetParent(SlotParent.transform);
            allSlots.Add(newSlot);
            newSlot.GetComponent<Button>().onClick.AddListener(delegate { MoveItem(newSlot); });
            newSlot.SetActive(false);
        }
        hoverYOffset = slotSize * 0.01f;
    }
    public void UpdateBackgroundLayout(int rows, int slots)
    {
        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);
        int colomns = slots / rows;
        int Index = 0;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colomns; x++)
            {

                GameObject newBackgroundSlot = allBackgroundSlot[Index];

                RectTransform BackgroundRect = newBackgroundSlot.GetComponent<RectTransform>();

                newBackgroundSlot.transform.SetParent(BackgroundParent.transform);
                BackgroundRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                BackgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                BackgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                newBackgroundSlot.SetActive(true);
                Index++;
              
            }
        }
    }
    public void UpdateLayout(List<Stack<ItemScript>> items ,int rows,int slots)
    {
        this.chestItems = items;
        this.chestSlot = slots;

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);
        int colomns = slots / rows;
        int index = 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colomns; x++)
            {

                GameObject newSlot = allSlots[index];
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.transform.SetParent(SlotParent.transform);
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);

                newSlot.transform.SetParent(SlotParent.transform);

              
                if (items.Count != 0 && items.Count >= index && items[index].Count > 0)
                {
                    newSlot.GetComponent<Slot>().AddItems(items[index]);
                }

                index++;
            }
        }
    }
    public override void Open()
    {
        base.Open();
        if (IsOpen)
        {
            MoveItemsFromChest();
        }
    }
    public void MoveItemsToChest()
    {
        chestItems.Clear();

        for (int i = 0; i < chestSlot; i++)
        {
            Slot tmpSlot = allSlots[i].GetComponent<Slot>();
            if (!tmpSlot.IsEmpty)
            {
                chestItems.Add(new Stack<ItemScript>(tmpSlot.Items));
                if (!IsOpen)
                {
                    tmpSlot.ClearSlot();        
                }
            }
            else
            {
                chestItems.Add(new Stack<ItemScript>());
            }
            if (!IsOpen)
            {
                allSlots[i].SetActive(false);
                allBackgroundSlot[i].SetActive(false);
            }
        }

    }
    public void MoveItemsFromChest()
    {
        for (int i = 0; i < chestSlot; i++)
        {
            if (chestItems.Count != 0 && chestItems.Count >= i && chestItems[i] != null && chestItems[i].Count > 0)
            {
                GameObject newSlot = allSlots[i];
                newSlot.GetComponent<Slot>().AddItems(chestItems[i]);
            }
        }
        for (int i = 0; i < chestSlot; i++)
        {
            allSlots[i].SetActive(true);
            allBackgroundSlot[i].SetActive(true);
        }
    }
    protected override IEnumerator FadeOut()
    {
        yield return StartCoroutine(base.FadeOut());
        MoveItemsToChest();
    }
    public override void SaveInventory()
    {
       
    }
    public override void LoadInventory()
    {   
    }   
}
