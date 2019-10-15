using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "CharacterData/Player", fileName = "NewPlayer")]
    public class Player : Entity
    {
        [Space]
        [Header("Player")]

        [SerializeField]
        private int currentFury;
        private int maxFury,
                    currentConspicuity,
                    maxConspicuity,
                    currentNoiseLevel,
                    maxNoiseLevel,
                    currentSmellLevel,
                    maxSmellLevel;

   
        [SerializeField]
        private float poisonRate, currentTiredLevel, maxTiredLevel,
                     currentDrink, maxDrink, currentFood, maxFood, overEat, currentXP, maxXP;
        private int Hunger;


        private int currentEat;

        public bool Thirst { get { return currentDrink > 50; } }
        public bool OverEated { get { return currentFood < 10; } }
        public bool Hungry { get { return currentFood > 70; } }
        public float CurrentXP
        {
            get
            {
                return currentXP;
            }

            set
            {
                currentXP = value < maxXP ? value : maxXP;
            }
        }

        public float MaxXP
        {
            get
            {
                return maxXP;
            }

            set
            {
                maxXP = value > currentXP ? value : currentXP;
            }
        }

        public float PoisonRate
        {
            get
            {

                return poisonRate - PoisonResistance > 0 ? poisonRate - PoisonResistance : 0;
            }

            set
            {
                poisonRate = value;
            }
        }

        public int CurrentFury
        {
            get
            {


                return currentFury;
            }

            set
            {
                currentFury = value < maxFury ? value : maxFury;
            }
        }

        public int MaxFury
        {
            get
            {
                return maxFury;
            }

            set
            {
                maxFury = value > currentFury ? value : currentFury;
            }
        }

        public int CurrentConspicuity
        {
            get
            {
                return currentConspicuity;
            }

            set
            {
                currentConspicuity = value < maxConspicuity ? value : maxConspicuity;
            }
        }

        public int MaxConspicuity
        {
            get
            {
                return maxConspicuity;
            }

            set
            {
                maxConspicuity = value > currentConspicuity ? value : currentConspicuity;
            }
        }

        public int CurrentNoiseLevel
        {
            get
            {
                return currentNoiseLevel;
            }

            set
            {
                currentNoiseLevel = value < maxNoiseLevel ? value : maxNoiseLevel;
            }
        }

        public int MaxNoiseLevel
        {
            get
            {
                return maxNoiseLevel;
            }

            set
            {
                maxNoiseLevel = value > currentNoiseLevel ? value : currentNoiseLevel;
            }
        }

        public int CurrentSmellLevel
        {
            get
            {
                return currentSmellLevel;
            }

            set
            {
                currentSmellLevel = value < maxNoiseLevel ? value : maxNoiseLevel;
            }
        }

        public int MaxSmellLevel
        {
            get
            {
                return maxSmellLevel;
            }

            set
            {
                maxSmellLevel = value > currentSmellLevel ? value : currentSmellLevel;
            }
        }

        public float CurrentTiredLevel
        {
            get
            {
                return currentTiredLevel;
            }

            set
            {

                currentTiredLevel = value < maxTiredLevel ? value : maxTiredLevel;
            }
        }

        public float MaxTiredLevel
        {
            get
            {
                return maxTiredLevel;
            }

            set
            {
                maxTiredLevel = value > currentTiredLevel ? value : currentTiredLevel;
            }
        }

        public float CurrentDrink
        {
            get
            {

                return currentDrink;
            }

            set
            {
                currentDrink = value < MaxDrink ? value : MaxDrink;
            }
        }

        public float MaxDrink
        {
            get
            {
                return maxDrink;
            }

            set
            {
                maxDrink = value > currentDrink ? value : currentDrink;
            }
        }

        public float CurrentFood
        {
            get
            {
                return currentFood;
            }

            set
            {
                currentFood = value < maxFood ? value : maxFood;
            }
        }

        public float MaxFood
        {
            get
            {
                return maxFood;
            }

            set
            {
                maxFood = value > currentFood ? value : currentFood;
                if (value >= 100)
                {
                    maxFood = 100;
                }
                else if (value <= currentFood)
                {
                    maxFood = currentFood;
                }
                else
                {
                    maxFood = value;
                }

            }
        }
    }
}