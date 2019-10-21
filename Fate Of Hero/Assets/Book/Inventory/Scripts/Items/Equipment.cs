using FourGames;
using UnityEngine;

namespace InventorySystem
{
    public class Equipment : Item
    {
        [Space]
        [Header("Equipment")]

        [SerializeField]
        protected int strength;

        [SerializeField]
        protected int dexterity,
                        intellect,
                        stamina,
                        stealth,
                        durability,
                        luck;



        [SerializeField]
        protected bool equipable;

        public bool Equipable => equipable;
        public int __Strength { get => strength; set => strength = value; }
        public int __Dexterity { get => dexterity; set => dexterity = value; }
        public int __Intellect { get => intellect; set => intellect = value; }
        public int __Stamina { get => stamina; set => stamina = value; }
        public int __Stealth { get => stealth; set => stealth = value; }
        public int __Durability { get => durability; set => durability = value; }
        public int __Luck { get => luck; set => luck = value; }

        public override void Use(Slot slot)
        {
            if (InventoryManager.Instance.CheckEquiped(slot))
            {
                InventoryManager.Instance.Inventory.Add(slot.Peek(), slot.ItemCount);
                slot.Clear();
                PlayerScript.Instance.SwordRender.enabled = false;


            }
            else
            {
                InventoryManager.Instance.CharacterPanel.Equip(slot);
                PlayerScript.Instance.SwordRender.enabled = true;



            }
        }
    }
}

