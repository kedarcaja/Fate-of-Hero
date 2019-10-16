using System.Collections;
using System.Collections.Generic;
using Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class Chest : MonoBehaviour
    {

        private Bag bag;

        CanvasGroup group;

        public Bag Bag => bag;

        [SerializeField]
        private Button takeAllButton;

        private void Awake()
        {
            group = GetComponentInParent<CanvasGroup>();
            bag = GetComponent<Bag>();

        }

        public void Open(ChestScript chest)
        {
            //takeAllButton.onClick.AddListener(() => chest.Bag.TakeAllToInventory());
            takeAllButton.onClick.RemoveAllListeners();

            Bag.Saver = chest.Bag.Saver;
            bag.MaxBagLevel = chest.Bag.MaxBagLevel;
            bag.Saver.Level = chest.Bag.MaxBagLevel;
            bag.SlotsInRow = chest.Bag.SlotsInRow;
            GetComponent<GridLayoutGroup>().constraintCount = chest.Bag.SlotsInRow;
            bag.DrawLayout();
            group.Active(true);
            
            CharacterPanel.Instance.GetComponent<CanvasGroup>().Deactive(true);

        }

        public void Close(ChestScript chest)
        {
            bag.Saver = null;
            group.Deactive(true);
            chest.Bag.DrawLayout();
            CharacterPanel.Instance.GetComponent<CanvasGroup>().Active(true);
        }
    }
}

