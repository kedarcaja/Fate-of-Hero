using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    #region Variables
    protected RectTransform inventoryRect;
    protected float inventoryWidth, inventoryHight;
    public int slots;
    public int rows;
    public float slotPaddingLeft, slotPaddingTop;
    public float slotSize;
    protected float hoverYOffset;
    public GameObject BackgroundParent, SlotParent;
    protected List<GameObject> allSlots;
    protected List<GameObject> allBackgroundSlot;
    public CanvasGroup canvasGroup;
    private bool fadingIn, fadingOut;
    public float fadeTime;
    private int emptySlots;
    int DrawBackgroundSlot;
    protected static GameObject playerRef;
    public int EmptySlot

    {
        get
        {
            return emptySlots;
        }

        set
        {
            emptySlots = value;
        }
    }   
    private bool isOpen;
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }

        set
        {
            isOpen = value;
        }
    }

    public bool FadingOut
    {
        get
        {
            return fadingOut;
        }

    }

    public static bool mouseInside = false;
    public bool instantClose = true;
    #endregion

    #region Unity Metod

    protected virtual void Start()
    {
        isOpen = false;
        playerRef = GameObject.Find("Player");
        InventoryManager.Instance.selectStackSize = InventoryManager.Instance.selectStackSize;
        CreateBackgroundLayout();
       
        InventoryManager.Instance.MovingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
    }
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            if (!mouseInside && InventoryManager.Instance.From != null)
            {
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
                foreach (ItemScript item in InventoryManager.Instance.From.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                    v *= 1;
                    GameObject tmpDrp = Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);
                    tmpDrp.AddComponent<ItemScript>();
                    tmpDrp.GetComponent<ItemScript>().Item = item.Item;
                }
                InventoryManager.Instance.From.ClearSlot();//

                if (InventoryManager.Instance.From.transform.parent == CharacterPanel.Instance.transform)
                {
                    CharacterPanel.Instance.CalcStats();
                }

                Destroy(GameObject.Find("Hover"));
                InventoryManager.Instance.To = null;
                InventoryManager.Instance.From = null;//
                
            }
            else if (!mouseInside && !InventoryManager.Instance.MovingSlot.IsEmpty)
            {
                foreach (ItemScript item in InventoryManager.Instance.MovingSlot.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    v *= 1;
                    GameObject tmpDrp = Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);
                    tmpDrp.AddComponent<ItemScript>();
                    tmpDrp.GetComponent<ItemScript>().Item = item.Item;
                    Destroy(GameObject.Find("Hover"));

                }
                InventoryManager.Instance.MovingSlot.ClearSlot();
                
            }
        }

        if (InventoryManager.Instance.HoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset);
            InventoryManager.Instance.HoverObject.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);
        }




    }
    public void OnDrag()
    {
        if (IsOpen)
        {
            MoveInventory();
        }   
    }
    public void PoiterExit()
    {
        mouseInside = false;
    }
    public void PointerEnter()
    {
        if (canvasGroup.alpha > 0)
        {
            mouseInside = true;
        }

    }
    public virtual void Open()
    {
        if (canvasGroup.alpha > 0)
        {
            StartCoroutine("FadeOut");
            PutItemBack();
            isOpen = false;
            HideToolTip();
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            StartCoroutine("FadeIn");
            isOpen = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public virtual void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();
        if (slot.GetComponentInParent<Inventory>().isOpen && !tmpSlot.IsEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            InventoryManager.Instance.visialTextObject.text = tmpSlot.CurrentItem.GetTooltip(this);
            InventoryManager.Instance.sizeTextObject.text = InventoryManager.Instance.visialTextObject.text;
            InventoryManager.Instance.tooltipObject.SetActive(true);
            float xPos = slot.transform.position.x + 100 + slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop;

            InventoryManager.Instance.tooltipObject.transform.position = new Vector3(xPos, yPos);
        }

    }
    public void HideToolTip()
    {
        InventoryManager.Instance.tooltipObject.SetActive(false);
    }
    public virtual void SaveInventory()
    {
        string content = string.Empty;
        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();
            if (!tmp.IsEmpty)
            {
                content += i + "-" + tmp.CurrentItem.Item.ItemName.ToString() + "-" + tmp.Items.Count.ToString() + ";";
            }
        }

        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.SetInt(gameObject.name + "slots", slots);
        PlayerPrefs.SetInt(gameObject.name + "rows", rows);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat(gameObject.name + "slotSize", slotSize);
       // PlayerPrefs.SetFloat(gameObject.name + "xPos", inventoryRect.position.x);
       // PlayerPrefs.SetFloat(gameObject.name + "yPos", inventoryRect.position.y);

        PlayerPrefs.Save();
    }
    public virtual void LoadInventory()
    {
        string content = PlayerPrefs.GetString(gameObject.name + "content");
        if (content != string.Empty)
        {
            slots = PlayerPrefs.GetInt(gameObject.name + "slots");
            rows = PlayerPrefs.GetInt(gameObject.name + "rows");
            slotPaddingLeft = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingLeft");
            slotPaddingTop = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingTop");
            slotSize = PlayerPrefs.GetFloat(gameObject.name + "slotSize");

            // inventoryRect.position = new Vector3(PlayerPrefs.GetFloat(gameObject.name + "xPos"), PlayerPrefs.GetFloat(gameObject.name + "yPos"), inventoryRect.position.z);

            CreateLayout();

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
                    allSlots[index].GetComponent<Slot>().AddItem(loadedItem.GetComponent<ItemScript>());

                    Destroy(loadedItem);
                }
            }
        }
    }
    private void MoveInventory()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, new Vector3(Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * InventoryManager.Instance.canvas.scaleFactor), Input.mousePosition.y + (inventoryRect.sizeDelta.y / 2 * InventoryManager.Instance.canvas.scaleFactor)), InventoryManager.Instance.canvas.worldCamera, out mousePos);

        transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(mousePos);
    }
    private void PutItemBack()
    {
        if (InventoryManager.Instance.From != null)
        {
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From = null;
        }

    }
    public void TrashItem()
    {
        if (InventoryManager.Instance.HoverObject != null && InventoryManager.Instance.From != null)
        {
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From.ClearSlot();
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.To = null;
            InventoryManager.Instance.From = null;
            InventoryManager.Instance.HoverObject = null;
            CharacterPanel.Instance.CalcStats();
           
            //emptySlot++;
        }
        else if (!InventoryManager.Instance.MovingSlot.IsEmpty)
        {
            InventoryManager.Instance.MovingSlot.ClearSlot();
            Destroy(GameObject.Find("Hover"));
        }
    }
    public void DropItems()
    {
        if (InventoryManager.Instance.From != null)
        {
            foreach (ItemScript item in InventoryManager.Instance.From.Items)
            {
                float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
                Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                v *= 1;
                GameObject tmpDrp = Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);
                tmpDrp.AddComponent<ItemScript>();
                tmpDrp.GetComponent<ItemScript>().Item = item.Item;

            }
            TrashItem();
        }
        else if (!InventoryManager.Instance.MovingSlot.IsEmpty)
        {
            foreach (ItemScript item in InventoryManager.Instance.MovingSlot.Items)
            {
                float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
                Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                v *= 1;
                GameObject tmpDrp = Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);
                tmpDrp.AddComponent<ItemScript>();
                tmpDrp.GetComponent<ItemScript>().Item = item.Item;
                
                Destroy(GameObject.Find("Hover"));
            }
            InventoryManager.Instance.MovingSlot.ClearSlot();
        }
        CharacterPanel.Instance.CalcStats();
    }
    public virtual void CreateBackgroundLayout() //Vyhreslení pozadí slotů
    {
        allBackgroundSlot = new List<GameObject>();

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);
        int colomns = slots / rows;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colomns; x++)
            {
                GameObject newBackgroundSlot = (GameObject)Instantiate(InventoryManager.Instance.BackgroundSlotPrefab);
                newBackgroundSlot.transform.SetParent(BackgroundParent.transform);
                RectTransform BackgroundRect = newBackgroundSlot.GetComponent<RectTransform>();
                newBackgroundSlot.name = "Background Slot";

                BackgroundRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                BackgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                BackgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                DrawBackgroundSlot++;
                allBackgroundSlot.Add(newBackgroundSlot);

                if (DrawBackgroundSlot == slots)
                {
                    CreateLayout();
                }
            }
        }
    }
    public virtual void CreateLayout() //Vykreslení slotů
    {
        if (allSlots != null)
        {
            foreach (GameObject go in allSlots)
            {
                Destroy(go);
            }
        }
        allSlots = new List<GameObject>();
        hoverYOffset = slotSize * 0.01f;
        EmptySlot = slots;
        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);
        int colomns = slots / rows;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colomns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);
                newSlot.transform.SetParent(SlotParent.transform);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                newSlot.name = "Item Slot";

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                allSlots.Add(newSlot);
                newSlot.GetComponent<Button>().onClick.AddListener(delegate { MoveItem(newSlot); } );
            }

        }
    }
    private void CreateHoverIcon()
    {
        InventoryManager.Instance.HoverObject = Instantiate(InventoryManager.Instance.IconPrefab);
        InventoryManager.Instance.HoverObject.GetComponent<Image>().sprite = InventoryManager.Instance.Clicked.GetComponent<Image>().sprite;
        InventoryManager.Instance.HoverObject.name = "Hover";

        RectTransform hoverTransform = InventoryManager.Instance.HoverObject.GetComponent<RectTransform>();
        RectTransform clickedTranform = InventoryManager.Instance.Clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTranform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTranform.sizeDelta.y);

        InventoryManager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

        InventoryManager.Instance.HoverObject.transform.localScale = InventoryManager.Instance.Clicked.gameObject.transform.localScale;

        InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString() : string.Empty;
    }
    public void SplitStack() //Oddělení zvoleného množství
    {
        InventoryManager.Instance.selectStackSize.SetActive(false);
        if (InventoryManager.Instance.SpliteAmount == InventoryManager.Instance.MaxStackCount)
        {
            MoveItem(InventoryManager.Instance.Clicked);
        }
        else if (InventoryManager.Instance.SpliteAmount > 0)
        {
            InventoryManager.Instance.MovingSlot.Items = InventoryManager.Instance.Clicked.GetComponent<Slot>().RemoveItem(InventoryManager.Instance.SpliteAmount);
            CreateHoverIcon();
        }
    }
    public void ChangeStackText(int i) //Změna hodnoty oddělených itemů ze stacku
    {
        InventoryManager.Instance.SpliteAmount += i;
        if (InventoryManager.Instance.SpliteAmount < 0)
        {
            InventoryManager.Instance.SpliteAmount = 0;
        }
        if (InventoryManager.Instance.SpliteAmount > InventoryManager.Instance.MaxStackCount)
        {
            InventoryManager.Instance.SpliteAmount = InventoryManager.Instance.MaxStackCount;
        }
        InventoryManager.Instance.StackText.text = InventoryManager.Instance.SpliteAmount.ToString();
    }
    public void MergeSize(Slot source, Slot destination)
    {
        int max = destination.CurrentItem.Item.MaxSize - destination.Items.Count;
        int count = source.Items.Count < max ? source.Items.Count : max;
        for (int i = 0; i < count; i++)
        {
            destination.AddItem(source.RemoveItem());
        }
        if (source.Items.Count == 0)
        {
            source.ClearSlot();
            Destroy(GameObject.Find("Hover"));
        }
    }
    public bool AddItem(ItemScript item)
    {
        if (item.Item.MaxSize == 1)
        {
           return PlaceEmpty(item);
        }
        else
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (!tmp.IsEmpty)
                {
                    if (tmp.CurrentItem.Item.ItemName == item.Item.ItemName && tmp.IsAvailable)
                    {
                        if (!InventoryManager.Instance.MovingSlot.IsEmpty && InventoryManager.Instance.Clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
                        {
                            continue;
                        }
                        else
                        {
                            tmp.AddItem(item);
                            return true;
                        }
                    }
                }
            }
            if (EmptySlot > 0)
            {
               return PlaceEmpty(item);
            }
        }
        return false;
    }
    private bool PlaceEmpty(ItemScript item)
    {
        if (EmptySlot > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();
                if (tmp.IsEmpty)
                {
                    tmp.AddItem(item);
                    return true;
                }
            }
        }
        Debug.Log("Full");
        return false;
    }
    public virtual void MoveItem(GameObject clicked)
    {
        if (isOpen)
        {
            CanvasGroup cg = clicked.transform.parent.GetComponent<CanvasGroup>();

            if (cg != null && cg.alpha > 0 || clicked.transform.parent.parent.GetComponent<CanvasGroup>())
            {
                InventoryManager.Instance.Clicked = clicked;

                if (!InventoryManager.Instance.MovingSlot.IsEmpty)
                {
                    Slot tmp = clicked.GetComponent<Slot>();

                    if (tmp.IsEmpty)
                    {
                        tmp.AddItems(InventoryManager.Instance.MovingSlot.Items);
                        InventoryManager.Instance.MovingSlot.Items.Clear();
                        Destroy(GameObject.Find("Hover"));
                    }
                    else if (!tmp.IsEmpty && InventoryManager.Instance.MovingSlot.CurrentItem.Item.ItemName == tmp.CurrentItem.Item.ItemName && tmp.IsAvailable)
                    {
                        MergeSize(InventoryManager.Instance.MovingSlot, tmp);
                    }


                }
                else if (InventoryManager.Instance.From == null && clicked.transform.parent.GetComponent<Inventory>().IsOpen && !Input.GetKey(KeyCode.LeftShift))
                {
                    if (!clicked.GetComponent<Slot>().IsEmpty && !GameObject.Find("Hover"))
                    {

                        InventoryManager.Instance.From = clicked.GetComponent<Slot>();
                        InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;
                        CreateHoverIcon();
                    }
                }
                else if (InventoryManager.Instance.To == null && !Input.GetKey(KeyCode.LeftShift))
                {
                    InventoryManager.Instance.To = clicked.GetComponent<Slot>();
                    Destroy(GameObject.Find("Hover"));
                }
                if (InventoryManager.Instance.To != null && InventoryManager.Instance.From != null)
                {
                    if (!InventoryManager.Instance.To.IsEmpty && InventoryManager.Instance.From.CurrentItem.Item.ItemName == InventoryManager.Instance.To.CurrentItem.Item.ItemName && InventoryManager.Instance.To.IsAvailable)
                    {
                        MergeSize(InventoryManager.Instance.From, InventoryManager.Instance.To);
                    }
                    else
                    {
                        Slot.SwapItems(InventoryManager.Instance.From, InventoryManager.Instance.To);
                    }


                    InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
                    InventoryManager.Instance.To = null;
                    InventoryManager.Instance.From = null;
                    Destroy(GameObject.Find("Hover"));
                }
            }

            if (CraftingBench.Instance.IsOpen)
            {
                CraftingBench.Instance.UpdatePreview();
            }
        }
        
    }
    protected virtual IEnumerator FadeOut()
    {
        if (!FadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlfa = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlfa, 0, progress);
                progress += rate * Time.deltaTime;
                if (instantClose)
                {
                    break;
                }
                yield return null;
            }
            canvasGroup.alpha = 0;
            instantClose = false;
            fadingOut = false;
        }

    }
    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlfa = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlfa, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
            fadingIn = false;
        }

    }
    #endregion
}