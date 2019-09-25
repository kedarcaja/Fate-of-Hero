using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

	[SerializeField]
	private TextMeshProUGUI description, gold, silver, copper, level, giver, location, xp, notes, rewardsText;
	[SerializeField]
	private GameObject questPartPref, questPref, itemRewardPref, nextArrow;
	[SerializeField]
	private Image line;
	[SerializeField]
	private Button mc_button;
	[SerializeField]
	private Transform questPartParent, itemRewardParent;
	[SerializeField]
	private Transform questLog;
		public GameObject QuestPref
	{
		get
		{
			return questPref;
		}
	}
	public GameObject QuestPartPref
	{
		get
		{
			return questPartPref;
		}
	}
	[SerializeField]
	private QuestPref main, secondary, treasure, profs, completed;

	public QuestPref Main
	{
		get
		{
			return main;
		}
	}

	public QuestPref Secondary
	{
		get
		{
			return secondary;
		}
	}

	public QuestPref Treasure
	{
		get
		{
			return treasure;
		}
	}

	public QuestPref Profs
	{
		get
		{
			return profs;
		}
	}

	public QuestPref Completed
	{
		get
		{
			return completed;
		}
	}
	public void SetQuestAsCompleted(Quest q)
	{
		q.GO.transform.parent = completed.QuestParent.transform;
	}

	public Quest currentlyOpenedQuest { get; private set; }
	public static QuestManager Instance;
	private void Awake()
	{
		Instance = FindObjectOfType<QuestManager>();
		HideQuests();
	}
	public void UpdateKillQuestParts(IID id)
	{
		foreach (QuestInteractionPart p in Resources.LoadAll("QuestSystem/QuestParts/KillParts"))
		{
			if (p.HaveSameID(id))
			{
				p.Interact();
			}
			if (p.Quest.Completed)
			{
				SetQuestAsCompleted(p.Quest);

			}

		}
	}
	public void UpdateInteractionQuestParts(IID id)
	{
		foreach (QuestInteractionPart p in Resources.LoadAll("QuestSystem/QuestParts/InteractionParts"))
		{
			if (p.HaveSameID(id))
			{
				p.Interact();

			}
			if (p.Quest.Completed)
			{
				SetQuestAsCompleted(p.Quest);

			}
		}
	}
	//public void UpdateCollectQuestParts(Item item)
	//{
	//	foreach (QuestCollectPart p in Resources.LoadAll("QuestSystem/QuestParts/CollectParts"))
	//	{
	//		if (item == p.CollectItem)
	//		{
	//			if (p.Quest.Completed)
	//			{
	//				SetQuestAsCompleted(p.Quest);
	//			}
	//		}
	//	}
	//}


	public void AddQuest(Quest quest)
	{

		Transform parent = null;
		switch (quest.Category)
		{
			case EQuestCategory.Main:
				parent = main.QuestParent.transform;
				break;
			case EQuestCategory.Secondary:
				parent = secondary.QuestParent.transform;
				break;
			case EQuestCategory.Treasure:
				parent = treasure.QuestParent.transform;
				break;
			case EQuestCategory.Profs:
				parent = profs.QuestParent.transform;
				break;
			case EQuestCategory.Complete:
				parent = completed.QuestParent.transform;
				break;
		}
		GameObject q = Instantiate(questPref);
		q.transform.parent = parent;

		q.transform.GetComponentInChildren<TextMeshProUGUI>().text = quest.name;
		q.transform.Find("Raycaster").GetComponent<Button>().onClick.AddListener(() => ShowQuest(quest));
		quest.GO = q;
		questLog.GetComponent<Animator>().SetTrigger("newQuest");

	}
	public void ShowQuest(Quest quest)
	{
	
		currentlyOpenedQuest = quest;
		nextArrow.SetActive(true);
		rewardsText.text = "Rewards: ";
		gold.transform.parent.GetComponentInChildren<Image>().color = Color.white;
		silver.transform.parent.GetComponentInChildren<Image>().color = Color.white;
		copper.transform.parent.GetComponentInChildren<Image>().color = Color.white;
		line.color = Color.white;
		description.text = quest.Description;
		for (int i = 0; i < questPartParent.childCount; i++)
		{
			Destroy(questPartParent.GetChild(i).gameObject);
		}
		foreach (QuestPart p in quest.Parts)
		{
			GameObject pis = Instantiate(questPartPref);
			pis.transform.parent = questPartParent;
			pis.transform.Find("Compulsory").GetComponent<Toggle>().isOn = p.Compulsory;
			pis.transform.Find("Completed").GetComponent<Toggle>().isOn = p.Completed();
			pis.transform.Find("Completed").transform.GetComponentInChildren<TextMeshProUGUI>().text = p.Description;
		}
		gold.text = quest.Rewards.Gold.ToString();
		silver.text = quest.Rewards.Silver.ToString();
		copper.text = quest.Rewards.Copper.ToString();

		for (int i = 0; i < itemRewardParent.childCount; i++)
		{
			Destroy(itemRewardParent.GetChild(i).gameObject);
		}
		for (int i = 0; i < quest.Rewards.Items.Count; i++)
		{
			GameObject p = Instantiate(itemRewardPref);
			//p.transform.parent = itemRewardParent;
			//p.transform.Find("background").GetComponent<Image>().sprite = quest.Rewards.Items[i].Item.QualityColor;
			//p.transform.Find("background").transform.Find("Sprite").GetComponent<Image>().sprite = quest.Rewards.Items[i].Item.Sprite;
			//p.transform.Find("background").transform.Find("Sprite").transform.Find("count").GetComponent<TextMeshProUGUI>().text = quest.Rewards.Items[i].Count.ToString();
			//p.transform.Find("background").transform.Find("Sprite").transform.Find("name").GetComponent<TextMeshProUGUI>().text = quest.Rewards.Items[i].Item.name;
			//p.transform.Find("background").transform.Find("Sprite").transform.Find("category").GetComponent<TextMeshProUGUI>().text = quest.Rewards.Items[i].Item.ItemType.ToString();
		}
		level.text = "Level: " + quest.Level.ToString();
		giver.text = "Zadavatel: " + quest.GiverName;
		location.text = "Lokace: " + quest.Location;
		xp.text = "XP: " + quest.Rewards.Xp.ToString();
		notes.text = quest.Notes;


	}

	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.N) && currentlyOpenedQuest)
		{
			currentlyOpenedQuest.TakeNote("bla bla bla");
		}

	}
	public void HideQuests()
	{
		currentlyOpenedQuest = null;
		nextArrow.SetActive(false);
		rewardsText.text = "";
		gold.transform.parent.GetComponentInChildren<Image>().color = new Color32(0, 0, 0, 0);
		silver.transform.parent.GetComponentInChildren<Image>().color = new Color32(0, 0, 0, 0);
		copper.transform.parent.GetComponentInChildren<Image>().color = new Color32(0, 0, 0, 0);
		line.color = new Color32(0, 0, 0, 0);
		description.text = "";
		for (int i = 0; i < questPartParent.childCount; i++)
		{
			Destroy(questPartParent.GetChild(i).gameObject);
		}

		gold.text = "";
		silver.text = "";
		copper.text = "";


		for (int i = 0; i < itemRewardParent.childCount; i++)
		{
			Destroy(itemRewardParent.GetChild(i).gameObject);
		}

		level.text = "";
		giver.text = "";
		location.text = "";
		xp.text = "";
		notes.text = "";
		mc_button.onClick.Invoke();

	}
}
[Serializable]
public struct QuestPref
{

	[SerializeField]
	private GameObject questParent;
	public GameObject QuestParent
	{
		get
		{
			return questParent;
		}
	}
}
