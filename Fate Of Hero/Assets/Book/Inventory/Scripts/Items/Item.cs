
using UnityEngine;
using System.Extensions;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/Items/Basic Item")]
    public class Item : ScriptableObject
    {
        [Header("Item")]
        [SerializeField]
        protected Sprite sprite;
        [SerializeField]
        protected ItemType mainType;

        [SerializeField]
        protected ItemType itemType;

        [SerializeField]
        protected EQuality quality;

        [SerializeField]
        protected ETooltipValues mainTooltipValue,
                                 otherTooltipValue;

        [SerializeField]
        protected int maxInSlot = 1;

        [SerializeField]
        [TextArea(0, 100)]
        protected string description;

        [SerializeField]
        protected int level;

        [Space]
        [SerializeField]
        [Range(0, 1000)]
        protected int sellGold,
                      sellSilver,
                      sellCopper;

        [Space]
        [SerializeField]
        [Range(0, 1000)]
        protected int buyGold,
                      buySilver,
                      buyCopper;

        [SerializeField]
        protected bool sellable;



        public int MaxInSlot { get => maxInSlot; }

        public ItemType MainType => mainType;

        public ItemType ItemType => itemType;

        public Sprite Sprite => sprite;


        public string MainTooltipValue => mainTooltipValue != ETooltipValues.none ? this.GetPropertyValueByName(mainTooltipValue.ToString()).ToString() : "";
        public string OtherTooltipValue => otherTooltipValue != ETooltipValues.none ? this.GetPropertyValueByName(otherTooltipValue.ToString()).ToString() : "";

        public bool Sellable => sellable;

        public int SellGold { get => sellGold; set => sellGold = value; }
        public int SellSilver { get => sellSilver; set => sellSilver = value; }
        public int SellCopper { get => sellCopper; set => sellCopper = value; }
        public int BuyGold { get => buyGold; set => buyGold = value; }
        public int BuySilver { get => buySilver; set => buySilver = value; }
        public int BuyCopper { get => buyCopper; set => buyCopper = value; }
        public string Description => description;
        public int Level => level;

        public virtual void Use(Slot slot)
        {

        }

        public Sprite QualityBackground => quality != EQuality.none ? InventoryManager.Instance.ItemQualityBackgrounds[(int)quality-1] : null;
        public string Quality => quality != EQuality.none ? quality.ToString() : "";

    }
    public enum EQuality
    {
        none, Comon, Uncomon, Rare, Epic, Mystic,Legendary,Mythical
    }

    public enum ItemType
    {
        GEM, CHAR, EQUIPMENT, WEAPON, CONSUMEABLE, MATERIAL, QUEST_OTHER, ALCHEMY, Chest, Helm, MeelWeapon, HealthPotion, Spaulder, Arm, Belt, Ring, // Capitalized are main types, other are subtipes
        Trausers, Necklance, Boots, SecondHand, Bow, Arrow, TwoHand, Food, Ore, Fuel, Shirt, Pickaxe, Shovel, FishingRod, Axe, Sickle, Hood
    }
    public enum ETooltipValues
    {
        none, __Armor, __Durability, __AttackSpeed,  __Damage, __Effect, __BuyGold
    }
}