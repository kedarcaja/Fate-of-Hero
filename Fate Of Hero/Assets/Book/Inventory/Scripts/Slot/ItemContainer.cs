using System.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemContainer : MonoBehaviour, IMouse
    {
        private Image img;
        private TextMeshProUGUI textCount;
        private Outline outline;
        private Slot slot;
        private Image qualityBg;
        private bool initialized = false;
        public bool UseRaycast { get; set; } = true;

        private void Start()
        {
            Init();
        }

        public void UpdateUI(Sprite sprite, Sprite quality, string countText, bool raycastEnable)
        {

            raycastEnable = UseRaycast && raycastEnable;
            if (!initialized)
            {
                Init();
            }
            img.sprite = sprite;
            textCount.text = countText;
            img.raycastTarget = raycastEnable;
            if (!raycastEnable)
            {
                outline.enabled = false;
            }

            if (qualityBg != null)
            {
                qualityBg.sprite = quality;
                qualityBg.color = qualityBg.sprite != null && slot.GetComponent<Image>() != null ? Vector4.one : Vector4.zero;
            }

        }


        private void Init()
        {
            slot = transform.parent.GetComponentInParent<Slot>();
            qualityBg = slot.transform.Find("Quality").GetComponent<Image>();
            img = GetComponent<Image>();
            textCount = GetComponentInChildren<TextMeshProUGUI>();
            outline = GetComponent<Outline>();
            initialized = true;
        }

        #region  Pointer Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (!InventoryManager.Instance.IsDraging)
                {
                    InventoryManager.Instance.Drag(slot);
                }
                else
                {
                    if (!InventoryManager.Instance.DragedFrom.Peek().EqualsInstanceProperties(slot.Peek()))
                    {
                        InventoryManager.Instance.SwapSlots(InventoryManager.Instance.DragedFrom, slot);
                    }
                    else
                    {
                        InventoryManager.Instance.MergeSlots(InventoryManager.Instance.DragedFrom, slot);
                    }
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    InventoryManager.Instance.StartSplit(slot);
                }
                else
                {
                    slot.Peek().Use(slot);
                }
            }
            InventoryManager.Instance.Tooltip.Hide();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            outline.enabled = true;
            InventoryManager.Instance.Tooltip.Show(slot);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            outline.enabled = false;
            InventoryManager.Instance.Tooltip.Hide();
        }
        #endregion
    }
}