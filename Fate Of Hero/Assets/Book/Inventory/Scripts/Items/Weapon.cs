using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/Items/Weapon Item")]
    public class Weapon : Equipment
    {
        [Space]
        [Header("Weapon")]

        [SerializeField]
        private WeaponType weaponType;
        [SerializeField]
        private WeaponRange weaponRange;
        [SerializeField]
        private WeaponWeight weaponWeight;
        [SerializeField]
        protected int damage,
                      fireAttack,
                      waterAttack,
                      coldAttack;
        [SerializeField]
        protected float attackSpeed;
        [SerializeField]
        protected float range;


        public int __Damage { get => damage; set => damage = value; }
        public int __FireAttack { get => fireAttack; set => fireAttack = value; }
        public int __WaterAttack { get => waterAttack; set => waterAttack = value; }
        public int __ColdAttack { get => coldAttack; set => coldAttack = value; }
        public float __AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float __Range { get => range; set => range = value; }
        public WeaponType WeaponType => weaponType;
        public WeaponRange WeaponRange => weaponRange;
        public WeaponWeight WeaponWeight => weaponWeight;

    }
    public enum WeaponRange
    {
        MEELEWEAPON, LONGRANGE
    }
    public enum WeaponWeight
    {
        OneHanded, TwoHanded,
    }
    public enum WeaponType
    {
        Longsword, shortSword, bow, crossBow, axe, hammer, warHammer, mace, spear, dagger, knife, throwingKnives, sling, chakram
    }
}

