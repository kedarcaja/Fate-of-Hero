using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum EQuestCategory { Main, Secondary, Treasure, Profs, Complete }
[CreateAssetMenu(menuName = "Quest/Quest", fileName = "NewQuest")]
public class Quest : ScriptableObject
{
	public void Add()
	{
		QuestManager.Instance.AddQuest(this);
	}
	public GameObject GO { get; set; }
	[SerializeField]
	private int level;
	[SerializeField]
	private List<QuestPart> parts;
	[TextArea(10, 30)]
	[SerializeField]
	private string description;
	[TextArea(10, 50)]
	public string Notes;
	public Action OnComplete;
	[SerializeField]
	private Rewards rewards;
	[SerializeField]
	private string giverName, location;

	public void TakeNote(string note)
	{
		Notes += note + " ";
	}
	public bool Completed
	{
		get
		{

			int x = 0;

			foreach (QuestPart p in parts.Where(e => e.Compulsory))
			{
				if (p.Completed())
				{
					x++;
				}
			}
			return x == parts.Where(p => p.Compulsory).Count();
		}


	}
	private void GetRewards()
	{
		foreach (ItemReward i in rewards.Items)
		{
	//		Inventory.Instance.AddItem(i.Item, i.Count);
		}
		//PlayerScript.Instance.XP += rewards.Xp;
		//PlayerScript.Instance.Gold += rewards.Gold;
		//PlayerScript.Instance.Silver += rewards.Silver;
		//PlayerScript.Instance.Copper += rewards.Copper;
	}
	public bool Accepted { get; set; }
	public bool Available { get; set; }
	[SerializeField]
	private EQuestCategory category;

	public List<QuestPart> Parts
	{
		get
		{
			return parts;
		}
	}

	public string Description
	{
		get
		{
			return description;
		}
	}

	public EQuestCategory Category
	{
		get
		{
			return category;
		}
	}

	public Rewards Rewards
	{
		get
		{
			return rewards;
		}
	}

	public int Level
	{
		get
		{
			return level;
		}


	}

	public string GiverName
	{
		get
		{
			return giverName;
		}


	}

	public string Location
	{
		get
		{
			return location;
		}
	}
}
[Serializable]
public struct Rewards
{
	[SerializeField]
	private int xp, gold, silver, copper;
	[SerializeField]
	private List<ItemReward> items;

	public List<ItemReward> Items
	{
		get
		{
			return items;
		}
	}

	public int Xp
	{
		get
		{
			return xp;
		}
	}
	public int Gold
	{
		get
		{
			return gold;
		}
	}
	public int Silver
	{
		get
		{
			return silver;
		}
	}
	public int Copper
	{
		get
		{
			return copper;
		}
	}
}
[Serializable]
public struct ItemReward
{
	public int Count;
	//public Item Item;
}
