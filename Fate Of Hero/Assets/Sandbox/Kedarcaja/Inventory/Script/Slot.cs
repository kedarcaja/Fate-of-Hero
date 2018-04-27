using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    #region Variables
    private Stack<ItemScript> items;
    public Stack<ItemScript> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }
    public Text stackTxt;
    public Sprite SlotEmpty;
    public Sprite slotHightlight;
    private CanvasGroup canvasGroup;
    public bool IsEmpty {
        get { return items.Count == 0; }
    }
    public bool IsAvailable {
        get { return CurrentItem.Item.MaxSize > items.Count; }
    }
    public ItemScript CurrentItem {
        get { return items.Peek(); }
    }

    public bool ClickAble
    {
        get
        {
            return clickAble;
        }

        set
        {
            clickAble = value;
        }
    }

    public ItemType canContain;
    private bool clickAble = true;
    #endregion

    #region Unity Metod
    void Awake() {
        items = new Stack<ItemScript>();
    }
    public void Start()
    {

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);

        if (transform.parent != null)
        {
            canvasGroup = transform.parent.GetComponent<CanvasGroup>();

            EventTrigger trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { transform.parent.GetComponent<Inventory>().ShowToolTip(gameObject); });
            trigger.triggers.Add(entry);

        }

        
    }
    public void AddItem(ItemScript item)
    {
        if (IsEmpty)
        {
            transform.parent.GetComponent<Inventory>().EmptySlot--;
        }
        items.Push(item);
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }
        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
        


    }
    public void AddItems(Stack<ItemScript> items)
    {
        this.items = new Stack<ItemScript>(items);
        
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
    }
    private void ChangeSprite(Sprite neutral, Sprite highlight) {
        GetComponent<Image>().sprite = neutral;
        SpriteState st = new SpriteState
        {
            highlightedSprite = highlight,
            pressedSprite = neutral
        };
        GetComponent<Button>().spriteState = st;
    }
    private void UseItem() {
        if (!IsEmpty)
        {
            if (transform.parent.GetComponent<Inventory>() is VendorInvetory)
            {
                if (CurrentItem.Item.BuyPrice <= Player.Instance.Gold && Player.Instance.inventory.AddItem(CurrentItem))
                {
                    Player.Instance.Gold -= CurrentItem.Item.BuyPrice;
                }
            }
            else if (VendorInvetory.Instance.IsOpen)
            {
                Player.Instance.Gold += CurrentItem.Item.SellPrice;
                RemoveItem();
            }
            else if (clickAble)
            {
                items.Peek().Use(this);
                stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

                if (IsEmpty)
                {
                    ChangeSprite(SlotEmpty, slotHightlight);
                    transform.parent.GetComponent<Inventory>().EmptySlot++;
                }
            }
        }        
    }
    public void ClearSlot() {
        items.Clear();

        ChangeSprite(SlotEmpty, slotHightlight);

        stackTxt.text = string.Empty;

        if (transform.parent != null)
        {
            transform.parent.GetComponent<Inventory>().EmptySlot++;
        }
        
    }
    public Stack<ItemScript> RemoveItem(int amount) {
        Stack<ItemScript> tmp = new Stack<ItemScript>();
        for (int i = 0; i < amount; i++)
        {
            tmp.Push(items.Pop());
        }
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        return tmp;
    }
    public ItemScript RemoveItem() {
        if (!IsEmpty)
        {
            ItemScript tmp;
            tmp = items.Pop();
            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (IsEmpty)
            {
                ClearSlot();
            }
            return tmp;
        }

        return null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && canvasGroup != null && canvasGroup.alpha > 0)
        {
            UseItem();
        }
    
       else if (eventData.button==PointerEventData.InputButton.Left&&Input.GetKey(KeyCode.LeftShift)&&!IsEmpty&&!GameObject.Find("Hover")&&transform.parent.GetComponent<Inventory>().IsOpen)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.tooltipObject.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);
            InventoryManager.Instance.selectStackSize.SetActive(true);
            InventoryManager.Instance.selectStackSize.transform.position = InventoryManager.Instance.tooltipObject.transform.TransformPoint(position);
            InventoryManager.Instance.SetStackInfo(items.Count);
        }
    }
    public static void SwapItems(Slot From, Slot To)
    {
       
        if (To != null && From != null)
        {
            bool calcStats = From.transform.parent == CharacterPanel.Instance.transform || To.transform.parent == CharacterPanel.Instance.transform;
            
            if (CanSwap(From,To))
            {
                Stack<ItemScript> tmpTo = new Stack<ItemScript>(To.Items);
                To.AddItems(From.Items);
                if (tmpTo.Count == 0)
                {
                    To.transform.parent.GetComponent<Inventory>().EmptySlot--;
                    From.ClearSlot();
                }
                else
                {
                    From.AddItems(tmpTo);
                }
               
            }
            if (calcStats)
            {
                CharacterPanel.Instance.CalcStats();
            }
        }
        
    }
    private static bool CanSwap(Slot From, Slot To)
    {
        ItemType fromType = From.CurrentItem.Item.ItemType;

        if (To.canContain == From.canContain) //swap two items in the inventory
        {
            return true;
        }
        if (fromType != ItemType.OFFHAND && To.canContain == fromType) //Equiping items
        {
            return true;
        }
        if (To.canContain == ItemType.GENERIC && (To.IsEmpty || To.CurrentItem.Item.ItemType == fromType)) //Dequipping an item
        {
            return true;
        }
        if (fromType == ItemType.MAINHAND && To.canContain == ItemType.GENERICWEAPON)//Equip main-hands
        {
            return true;
        }
        if (fromType == ItemType.TWOHAND && To.canContain == ItemType.GENERICWEAPON && CharacterPanel.Instance.OffHandSlot.IsEmpty)//Equip two-hands
        {
            return true;
        }
        if (fromType == ItemType.OFFHAND && (To.IsEmpty || To.CurrentItem.Item.ItemType == ItemType.OFFHAND) && (CharacterPanel.Instance.WeaponSlot.IsEmpty || CharacterPanel.Instance.WeaponSlot.CurrentItem.Item.ItemType != ItemType.TWOHAND))
        {
            return true;
        }
        return false;
    }
    #endregion
}
