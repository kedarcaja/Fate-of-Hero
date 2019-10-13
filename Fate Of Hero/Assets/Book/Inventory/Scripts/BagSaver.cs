using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/BagSaver", fileName = "NewBagSaver")]
    [Serializable]
    public class BagSaver : ScriptableObject
    {
        [SerializeField]
        private ItemType category;
        [SerializeField]
        private bool isGeneric = true;
        [SerializeField]
        private List<SlotSaver> slots = new List<SlotSaver>();
        [SerializeField]
        private int level = 1;
        [SerializeField]
        private bool staticLayout;


        public ItemType Category => category;
        public List<SlotSaver> Slots { get => slots; set => slots = value; }
        public int Level { get => level; set => level = value; }
        public bool StaticLayout => staticLayout;
        public bool IsGeneric { get => isGeneric; }


        #region  Unity Editor
#if UNITY_EDITOR
        [ContextMenu("GENERATE_SLOTS-Char")]
        public void GenerateBag()
        {
            List<SlotSaver> ss = new List<SlotSaver>();
            for (int i = 0; i < 36; i++)
            {
                SlotSaver s = ScriptableObjectUtility.CreateAsset<SlotSaver>("CBC0" + (i + 1));
                ss.Add(s);
            }
            for (int i = 0; i < ss.Count; i++)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(ss[i]), "CBC0" + (i + 1));
            }
            AssetDatabase.SaveAssets();
        }
#endif
        #endregion
    }
}