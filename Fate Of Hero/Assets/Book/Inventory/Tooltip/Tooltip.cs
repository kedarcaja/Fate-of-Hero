using TMPro;
using UnityEngine;
using Unity.Extensions;
using UnityEngine.UI;

namespace InventorySystem
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI itemName, itemDescription, itemMainValue, itemOtherValue, itemLevel, itemQuality, itemSellGold, itemSellSilver, itemSellCopper, itemStats, itemType;

        [Space]
        [SerializeField]
        private Slot slot;
        [SerializeField]
        private SlotSaver slotEmptySaver;

        [SerializeField]
        private Text descriptionSizer, statsSizer;

        private CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Set(Slot slot)
        {
            if (slot == null)
            {
              
               
                itemName.text = 
                itemDescription.text = 
                itemMainValue.text =
                itemOtherValue.text =
                itemLevel.text = 
                itemQuality.text =
                itemSellGold.text =
                itemSellSilver.text =
                itemSellCopper.text =
                itemStats.text =
                itemType.text =
                descriptionSizer.text =
                statsSizer.text = "";
  
                this.slot.Saver = slotEmptySaver;
            }
            else
            {
                itemName.text = slot.Peek().name;
                itemDescription.text = slot.Peek().Description;
                itemMainValue.text = slot.Peek().MainTooltipValue;
                itemOtherValue.text = slot.Peek().OtherTooltipValue;
                itemLevel.text = slot.Peek().Level.ToString();
                itemQuality.text = slot.Peek().Quality;
                itemSellGold.text = slot.Peek().SellGold.ToString();
                itemSellSilver.text = slot.Peek().SellSilver.ToString();
                itemSellCopper.text = slot.Peek().SellCopper.ToString();
                itemStats.text = InventoryManager.Instance.SlotComparsionWithCharacterPanel(slot);
                itemType.text = slot.Peek().ItemType.ToString();
                descriptionSizer.text = slot.Peek().Description;
                statsSizer.text = InventoryManager.Instance.SlotComparsionWithCharacterPanel(slot);

                slot.ShareVisual(this.slot);
            }
        }
        public void Clear()
        {
            Set(null);
        }
        public void Show(Slot slot)
        {
            if (!canvasGroup.IsActive(true))
            {
                canvasGroup.Active(true);
                Set(slot);

                Vector3 pos;
                Canvas can = FindObjectOfType<Canvas>();
                RectTransformUtility.ScreenPointToWorldPointInRectangle(can.transform as RectTransform, Input.mousePosition, can.worldCamera, out pos);
                transform.position = pos;
            }
        }
        public void Hide()
        {
            if (canvasGroup.IsActive(true))
            {
                canvasGroup.Deactive(true);
                Clear();
            }
        }

    }
}
