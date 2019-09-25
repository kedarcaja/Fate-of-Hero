using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName = "CharacterData/Player", fileName = "NewPlayer")]
public class Player : Entity
{
	[SerializeField]
	private int currentFury, maxFury, currentConspicuity, maxConspicuity,
				currentNoiseLevel, maxNoiseLevel, currentSmellLevel, maxSmellLevel;

	[SerializeField]
	private BarStats XPBar, healthBar, staminaBar, furyBar, conspicuityBar, noisyBar, smellBar, tiredBar, foodBar, drinkBar;

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
			XPBar.CurrentVal = currentXP;
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
			XPBar.MaxVal = maxXP;
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


			furyBar.CurrentVal = currentFury;
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
			furyBar.MaxVal = maxFury;
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
			conspicuityBar.CurrentVal = currentConspicuity;
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
			conspicuityBar.MaxVal = maxConspicuity;
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
			noisyBar.CurrentVal = currentNoiseLevel;
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
			noisyBar.MaxVal = maxNoiseLevel;
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
			smellBar.CurrentVal = currentSmellLevel;
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
			smellBar.MaxVal = maxSmellLevel;
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
			tiredBar.CurrentVal = currentTiredLevel;
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
			tiredBar.MaxVal = maxTiredLevel;
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
            
			drinkBar.CurrentVal = currentDrink;
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
			drinkBar.MaxVal = maxDrink;
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
			foodBar.CurrentVal = currentFood;
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
			foodBar.MaxVal = maxFood;
			return maxFood;
		}

		set
		{
			maxFood = value > currentFood ? value : currentFood;
			if (value >= 100)
			{
				maxFood = 100;
			}
			else if (value<=currentFood)
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
