using UnityEngine;
using UnityEngine.Events;
namespace Data
{
    public abstract class Entity : Character
    {
        [SerializeField]
        private int armor;
        [SerializeField]
        private float regeneration;
        [SerializeField]
        private float chanceToCritical, chanceToStunt;
        public int Armor
        {
            get
            {
                return armor;
            }

            set
            {
                armor = value;
            }
        }
        public float Regeneration
        {
            get
            {
                return regeneration;
            }

            set
            {
                regeneration = value < 60 ? value : 60; ;
            }
        }
        public float ChanceToCritical
        {
            get
            {
                return chanceToCritical;
            }

            set
            {
                chanceToCritical = value < 40 ? value : 40;
            }
        }
        public float ChanceToStunt
        {
            get
            {
                return chanceToStunt;
            }

            set
            {
                chanceToStunt = value < 60 ? value : 60;
            }
        }

    }
}