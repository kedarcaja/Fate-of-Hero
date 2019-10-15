using FourGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        #region  Variables
        //items
        [SerializeField]
        private List<Sprite> itemQualityBackgrounds = new List<Sprite>();
        [SerializeField]
        private List<Color> itemQualityBackgroundsColors = new List<Color>();


        public List<Color> ItemQualityBackgroundsColors { get => itemQualityBackgroundsColors; }
        public List<Sprite> ItemQualityBackgrounds { get => itemQualityBackgrounds; }

        //slot
        [SerializeField]
        private Sprite transparentItemSlot, defaultEmptySlotSprite;
        [SerializeField]
        private GameObject slotTemplate, splitUI;
        [SerializeField]
        private Slot bridgeSlot, splitSlot;
        private Slot dragedFrom, splitFrom;
        [SerializeField]
        private SlotSaver dragSlotSaver;

        public Sprite TransparentItemSlot => transparentItemSlot;
        public GameObject SlotTemplate => slotTemplate;
        public GameObject DragSlot { get; private set; }
        public Slot DragedFrom => dragedFrom;
        public Slot SplitFrom => splitFrom;
        public Sprite DefaultEmptySlotSprite => defaultEmptySlotSprite;



        //tooltip
        [SerializeField]
        private ButtonSlider splitSlider;
        [SerializeField]
        private Tooltip tooltip;
        public Tooltip Tooltip => tooltip;
        //other
        public bool IsDraging { get; private set; }
        public bool IsSplitting { get; private set; }
        [SerializeField]
        private CanvasGroupSwitchsGroup inventoryCategoriesSwitches;
        private bool isDragingFromSplit = false;

        [SerializeField]
        private CharacterPanel characterPanel;
        [SerializeField]
        private Inventory inventory;



        public Inventory Inventory => inventory;
        public CharacterPanel CharacterPanel => characterPanel;


        [Space]
        [Header("Drop")]
        [SerializeField]
        private List<BagSaver> dropSavers = new List<BagSaver>();

        [SerializeField]
        private GameObject dropSackFyzical;

        [SerializeField]
        private Chest chest;
        [SerializeField]
        private List<ChestScript> drops = new List<ChestScript>();



        public static InventoryManager Instance { get; private set; }
        public Chest Chest => chest;

        #endregion


        #region  Unity Editor Variables
        [Space(10)]
        [Header("Editor")]

        [SerializeField]
        private int generateSlotCount;

        [SerializeField]
        private int generateFolderCount, generateBagCount;

        [SerializeField]
        private string generateSlotName, generateFolderName, generateBagName;

        [Space]
        [SerializeField]
        private string folderPath;

        #endregion

        #region  Unity Methods
        private void Awake()
        {
            Instance = FindObjectOfType<InventoryManager>();
            splitSlider = splitUI.GetComponentInChildren<ButtonSlider>();
        }

        void Update()
        {
            if (IsDraging)
            {
                Vector3 pos;
                Canvas can = FindObjectOfType<Canvas>();
                RectTransformUtility.ScreenPointToWorldPointInRectangle(can.transform as RectTransform, Input.mousePosition, can.worldCamera, out pos);
                DragSlot.transform.position = pos;
            }
        }
        #endregion

        #region Inventory Methods

        public Bag GetCurrentlyOpenedBag()
        {
            return inventoryCategoriesSwitches.Current.Target.GetComponent<Bag>();
        }

        #endregion

        #region  CharacterPanel Methods

        public bool CheckEquiped(Slot slot)
        {
            return !slot.IsEmpty() && characterPanel.Equipment.BagForItem(slot.Peek()).Saver.Slots.Contains(slot.Saver);
        }

        public string SlotComparsionWithCharacterPanel(Slot slot)
        {
            Slot s = CharacterPanel.Equipment.BagForItem(slot.Peek()).SlotForItem(slot.Peek());
            if (s == null)
            {
                s = CharacterPanel.Equipment.BagForItem(slot.Peek()).SlotForItemToSwap(slot.Peek());
            }
            return slot.GetItemStats(s);
        }

        #endregion

        #region D&D Methods
        public void Drag(Slot from)
        {
            if (!IsSplitting)
            {
                inventoryCategoriesSwitches.DisableAll();
                inventoryCategoriesSwitches.Check(inventoryCategoriesSwitches.Switchs.Find(o => o.Target.GetComponent<Bag>().CanContain(from.Peek())));
                dragedFrom = from;
                DragSlot = Instantiate(slotTemplate);
                Slot s = DragSlot.GetComponent<Slot>();
                if (s.GetComponent<CanvasGroup>() != null)
                {
                    Destroy(s.GetComponent<CanvasGroup>());
                }
                Destroy(s.GetComponent<Image>());
                s.transform.Find("Quality").GetComponent<Image>().enabled = false;
                s.GetComponent<Outline>().effectDistance = Vector2.zero;
                s.GetComponentInChildren<Outline>().effectDistance = Vector2.zero;
                s.ItemContainer.UseRaycast = false;


                DragSlot.transform.SetParent(GameObject.Find("Book").transform);
                DragSlot.transform.SetAsLastSibling();
                DragSlot.name = "DragSlot";

                from.DragStart();
                if (isDragingFromSplit)
                {
                    s.Saver = dragSlotSaver;
                    s.Clear();
                    s.Add(from.Peek(), (int)splitSlider.GetValue());
                }
                else
                {
                    from.ShareVisual(s);
                }
                IsDraging = true;
            }
        }
        public void DropToEmptySlot(Slot to)
        {
            if (isDragingFromSplit)
            {
                Slot s = DragSlot.GetComponent<Slot>();
                if (to.CanContain(s.Peek()))
                {
                    to.Add(s.Peek(), s.ItemCount);
                }
            }
            else
            {
                if (to.CanContain(dragedFrom.Peek()))
                {
                    to.Add(dragedFrom.Peek(), dragedFrom.ItemCount);
                    dragedFrom.Clear();
                }
            }

            StopDrag();
        }
        public void MergeSlots(Slot from, Slot to)
        {
            int space = to.Peek().MaxInSlot - to.ItemCount;

            if (from.ItemCount <= space)
            {

                to.Add(from.Peek(), from.ItemCount);
                from.Clear();
            }
            else
            {
                to.Add(from.Peek(), space);

                if (isDragingFromSplit)
                {
                    splitFrom.Remove(space);
                    splitFrom.Add(from.Peek(), DragSlot.GetComponent<Slot>().ItemCount);
                }
                else
                {
                    from.Remove(space);
                }
            }

            if (IsDraging)
            {
                StopDrag();

            }
        }
        public void SwapSlots(Slot from, Slot to)
        {
            bridgeSlot.Clear();
            bridgeSlot.Add(from.Peek(), from.ItemCount);
            from.Clear();
            from.Add(to.Peek(), to.ItemCount);
            to.Clear();
            to.Add(bridgeSlot.Peek(), bridgeSlot.ItemCount);
            bridgeSlot.Clear();

            if (IsDraging)
            {
                StopDrag();
            }
        }

        private void StopDrag()
        {
            if (IsDraging)
            {
                dragedFrom.DragEnd();

                if (!bridgeSlot.IsEmpty())
                {
                    bridgeSlot.Clear();
                }

                IsDraging = false;
                dragedFrom = null;
                splitFrom = null;
                Destroy(DragSlot);
                isDragingFromSplit = false;
                inventoryCategoriesSwitches.EnableAll();
            }
        }
        #endregion

        #region  Split Methods

        public void StartSplit(Slot from)
        {
            if (!IsSplitting && from.ItemCount > 1 && !IsDraging)
            {
                splitUI.SetActive(true);
                splitSlider.SetSlider(true, 0, from.ItemCount, 1);
                from.ShareVisual(splitSlot);
                IsSplitting = true;
                splitFrom = from;
                from.DragStart();
            }
        }
        public void EndSplit()
        {
            if (IsSplitting)
            {
                IsSplitting = false;

                if (splitSlider.GetValue() > 0)
                {
                    isDragingFromSplit = true;
                    Drag(splitFrom);
                    splitFrom.Remove((int)splitSlider.GetValue());
                }

                splitUI.SetActive(false);
                splitFrom.DragEnd();
                inventoryCategoriesSwitches.EnableAll();
            }
        }
        #endregion

        #region Drop Methods
        public void DropItemFromSlot(Slot from)
        {
            for (int i = 0; i < from.ItemCount; i++)
            {
                DropItem(from.Peek());

            }
            from.Clear();
        }
        public void DropItem(Item item)
        {
            Instantiate(dropSackFyzical, FindObjectOfType<PlayerScript>().transform.forward, Quaternion.identity).GetComponent<Bag>();
        }
        public void DropDraged()
        {
            if (ExistingSaverDrop() || NewSaverForDrop())
            {
            
                ChestScript c = ExistingSaverDrop();
                if (c == null)
                {
                    // spawn object
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 1.1f);
                    Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                     c = Instantiate(dropSackFyzical, FindObjectOfType<PlayerScript>().transform.position + V, Quaternion.identity).GetComponent<ChestScript>();
                     c.Bag.Saver = NewSaverForDrop();
                    drops.Add(c);
                    c.Bag.MaxBagLevel = 1;
                    c.Bag.Saver.Level = 1;
                    c.Bag.SlotsInRow = 6;

                }
                else
                {
                    c.Bag.Saver = ExistingSaverDrop().Bag.Saver;
                }

              
                c.Bag.DrawLayout();
                c.Bag.Add(dragedFrom.Peek(), dragedFrom.ItemCount);
                dragedFrom.Clear();
            }
            StopDrag();
        }
        public BagSaver NewSaverForDrop()
        {
            return dropSavers.Find(d => !d.Slots.Exists(s => s.Items.Count > 1));
        }
        public ChestScript ExistingSaverDrop()
        {
            foreach (ChestScript c in drops)
            {
                Vector3 dp = c.transform.position;
                Vector3 pp = FindObjectOfType<PlayerScript>().transform.position;

                if (Vector3.Distance(pp, dp) <= 10 && c.Bag.Saver.Slots.Exists(e=>e.Items.Count == 0))
                {
                    return c;
                }
            }
            return null;
        }
       
        #endregion

        #region  Unity Editor
#if UNITY_EDITOR
        [ContextMenu("GENERATE_Items")]
        public void GenerateItems()
        {
            List<Weapon> w = new List<Weapon>();
            List<Consumeable> c = new List<Consumeable>();
            List<Armor> a = new List<Armor>();
            for (int i = 0; i < 3; i++)
            {
                Weapon ww = ScriptableObjectUtility.CreateAsset<Weapon>("Weapon" + (i + 1));
                w.Add(ww);
                Consumeable cc = ScriptableObjectUtility.CreateAsset<Consumeable>("Consumeable" + (i + 1));
                c.Add(cc);
                Armor aa = ScriptableObjectUtility.CreateAsset<Armor>("Armor" + (i + 1));
                a.Add(aa);
            }
            for (int i = 0; i < w.Count; i++)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(w[i]), "Weapon " + (i + 1));
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(c[i]), "Consumeable " + (i + 1));
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(a[i]), "Armor " + (i + 1));
            }
            AssetDatabase.SaveAssets();
        }
        [ContextMenu("GenerateFolders")]
        public void GenerateFolders()
        {
            for (int i = 0; i < generateBagCount; i++)
            {
                AssetDatabase.CreateFolder(folderPath, generateFolderName + (i + 1).ToString());
            }
            AssetDatabase.SaveAssets();
        }
        [ContextMenu("GenerateBags")]
        public void GenerateBags()
        {
            List<BagSaver> ss = new List<BagSaver>();
            for (int i = 0; i < generateBagCount; i++)
            {
                ss.Add(ScriptableObjectUtility.CreateAsset<BagSaver>("bag"));
            }
            for (int i = 0; i < ss.Count; i++)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(ss[i]), generateBagName + (i + 1));
            }
        }
        [ContextMenu("GenerateSlots")]
        public void GenerateSlots()
        {
            List<SlotSaver> ss = new List<SlotSaver>();
            for (int i = 0; i < generateSlotCount; i++)
            {
                ss.Add(ScriptableObjectUtility.CreateAsset<SlotSaver>("slot"));
            }
            for (int i = 0; i < ss.Count; i++)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(ss[i]), generateSlotName + (i + 1));
            }
        }
#endif
        #endregion


    }
}