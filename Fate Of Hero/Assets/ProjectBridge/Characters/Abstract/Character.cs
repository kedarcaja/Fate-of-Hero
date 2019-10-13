namespace Data
{
    using UnityEngine;
    public enum ECharacterClass { Warior, Archer }
    public enum EWork { Lamberjack, Miner, Baker, InnKeeper }
    public enum EMood { Angry, Scared, Sad, Happy }
    public class Character : ScriptableObject
    {

        [HideInInspector]
        public ECharacterClass CharacterClass;
        [HideInInspector]
        public EWork Work;
        [HideInInspector]
        public EMood Mood;
        [HideInInspector]
        public Sprite Portait;
        [HideInInspector]
        public Color Color;
        [HideInInspector]
        public int Level;
        public override string ToString()
        {

            return string.Format("<color=#" + ColorUtility.ToHtmlStringRGBA(Color) + ">" + name + "</color>");
        }

        #region Stats

        [Tooltip("CombatRadius")]
        [SerializeField]
        protected float combatRadius;
        [Tooltip("")]
        [SerializeField]
        protected float interactionRadius;

        [SerializeField]
        protected float health, maxHealth, stamina, maxStamina, minDamage, maxDamage;

        protected float damage;

        [Tooltip("FireResistance")]
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        protected float fireResistance;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("WaterResistance")]
        protected float waterResistance;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("ColdResistance")]
        protected float coldResistance;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("LightResistance")]
        protected float lightResistance;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("PoisonResistance")]
        protected float poisonResistance;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("MagicResistance")]
        protected float magicResistance;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("Influence")]
        protected float influence;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("Talking")]
        protected float talking;
        [SerializeField]
        [Range(0, 100)]
        [Header("Staty [%]")]
        [Tooltip("Luck")]
        protected float luck;

        [SerializeField]
        [Tooltip("Strength")]
        protected int strength;
        [SerializeField]
        [Tooltip("Agility")]
        protected int agility;
        [SerializeField]
        [Tooltip("Intellect")]
        protected int intellect;
        [SerializeField]
        [Tooltip("Charisma")]
        protected int charisma;
        [SerializeField]
        [Tooltip("RunSpeed")]
        protected int runSpeed;

        //  public List<CharacterScript> Followers = new List<CharacterScript>();
        public const int KEDAR = 5;
        [Tooltip("WalkSpeed")]
        [SerializeField]
        protected int walkSpeed;
        public int WalkSpeed { get { return walkSpeed; } }
        public float Health
        {
            get { return health; }
            set
            {
                if (value > maxHealth) health = maxHealth;
                else if (value <= 0) health = 0;
                else health = value;
            }
        }
        public float Stamina
        {
            get { return stamina; }
            set
            {
                if (value >= maxStamina) stamina = maxStamina;
                else if (value <= 0) stamina = 0;
                else stamina = value;
            }
        }
        public float MinDamage
        {
            get { return minDamage; }
            set { minDamage = value >= maxDamage ? maxDamage : minDamage; }
        }
        public float MaxDamage
        {
            get { return maxDamage; }
            set { maxDamage = value <= minDamage ? minDamage : value; }
        }
        public float Damage
        {
            get { return damage; }
            set { damage += value; }
        }

        public float MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value >= health ? value : health; }
        }

        public float MaxStamina
        {
            get { return maxStamina; }
            set { maxStamina = value >= stamina ? value : stamina; }
        }
        public float Influence
        {
            get { return influence; }
            set { influence = value <= 60 ? value : 60; }
        }
        public float Talking
        {
            get { return talking; }
            set { talking = value <= 60 ? value : 60; }
        }
        public float Luck
        {
            get { return luck; }
            set { luck = value <= 50 ? value : 50; }
        }
        public float FireResistance
        {
            get { return fireResistance; }
            set { fireResistance = value <= 60 ? value : 60; }
        }
        public float WaterResistance
        {
            get { return waterResistance; }
            set { waterResistance = value <= 60 ? value : 60; }
        }
        public float ColdResistance
        {
            get { return coldResistance; }
            set { coldResistance = value <= 60 ? value : 60; }
        }

        public float LightResistance
        {
            get { return lightResistance; }
            set { lightResistance = value <= 60 ? value : 60; }
        }
        public float PoisonResistance
        {
            get { return poisonResistance; }
            set { poisonResistance = value <= 60 ? value : 60; }
        }
        public float MagicResistance
        {
            get { return magicResistance; }
            set { magicResistance = value <= 60 ? value : 60; }
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value > 0 ? value : 0; }
        }
        public int Agility
        {
            get { return agility; }
            set { agility = value > 0 ? value : 0; }
        }
        public int Intellect
        {
            get { return intellect; }
            set { intellect = value > 0 ? value : 0; }
        }
        public int Charisma
        {
            get { return charisma; }
            set { charisma = value > 0 ? value : 0; }
        }

        public int RunSpeed { get { return runSpeed; } set { runSpeed = value; } }

        public bool IsAlive { get { return health > 0; } }

        public float CombatRadius { get => combatRadius > 0 ? combatRadius : 0; set => combatRadius = value; }
        public float InteractionRadius { get => interactionRadius > 0 ? interactionRadius : 0; set => interactionRadius = value; }

        #endregion

    }
}