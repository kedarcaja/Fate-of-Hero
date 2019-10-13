using UnityEngine;

namespace InventorySystem
{
    public enum ArmorWeight
    {
        Basic, cloth, LeatherArmor, ChainArmor, PlateArmor
    }

    [CreateAssetMenu(menuName = "Inventory/Items/Armor Item")]
    public class Armor : Equipment
    {
        [Space]
        [Header("Armor")]

        [SerializeField]
        private ArmorWeight armorWeight;

        [SerializeField]
        private int resistanceToCuttingWeapon,
                    resistanceToStabbingWeapons,
                    arrowResistance,
                    resistanceToBluntWeapons,
                    fireResistance,
                    waterResistance,
                    frostResistance,
                    poisonResistance,
                    magicResistance,
                    damageResistance,
                    armor;

        public ArmorWeight ArmorWeight => armorWeight;
        public int __ResistanceToCuttingWeapon { get => resistanceToCuttingWeapon; set => resistanceToCuttingWeapon = value; }
        public int __ResistanceToStabbingWeapons { get => resistanceToStabbingWeapons; set => resistanceToStabbingWeapons = value; }
        public int __ArrowResistance { get => arrowResistance; set => arrowResistance = value; }
        public int __ResistanceToBluntWeapons { get => resistanceToBluntWeapons; set => resistanceToBluntWeapons = value; }
        public int __FireResistance { get => fireResistance; set => fireResistance = value; }
        public int __WaterResistance { get => waterResistance; set => waterResistance = value; }
        public int __FrostResistance { get => frostResistance; set => frostResistance = value; }
        public int __PoisonResistance { get => poisonResistance; set => poisonResistance = value; }
        public int __MagicResistance { get => magicResistance; set => magicResistance = value; }
        public int __DamageResistance { get => damageResistance; set => damageResistance = value; }
        public int __Armor { get => armor; set => armor = value; }
    }

}
