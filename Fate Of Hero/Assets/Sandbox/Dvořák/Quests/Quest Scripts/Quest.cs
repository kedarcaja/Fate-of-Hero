using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour {
	public  enum EQuestType { Main = 0 , Other = 1, Profesion = 2, Aktivity = 3, Completed = 4 }
	public EQuestType questType;
	private GameObject coulomb;
	private int coulombIndex;
	[SerializeField]
	private string questName;
	[TextAreaAttribute(1, 1)]
	public string shortInfo;
	[HideInInspector]
	private Text text;
	private Sprite questTypeIcon;
	private QuestValues values;
	[SerializeField]
	[TextAreaAttribute(12,2000)]
	private string questContent;
	public bool completed, accepted, enable, isChoosed,isTrigged,isSettedAsCurrent;

	private GameObject quest,questIcon;
	private Button btn;
	private QuestManager manager;
	
	private void Start()
	{
		manager = GetComponent<QuestManager>();
		coulombIndex = (int)(questType);// getting int value from enum
		values = FindObjectOfType<QuestValues>();
		coulomb = values.coulombs[coulombIndex];
		SetCurrentAsEmpty();

	}

	private void Update()
	{

		
		values.close.onClick.AddListener(()=>HideQuest());
		
		if (enable)
		{
			showQuestIcon();
		
			if (this.isTrigged&&!this.accepted)
			{
				
					ShowQuest();
				


			}
			if (this.accepted)
			{

				btn.onClick.AddListener(() => ShowQuest());
				btn.onClick.AddListener(() => this.isChoosed = true);
				if (isChoosed&&!completed)
				{

					SetButtons(false, true);
					values.setAsCurrent.onClick.AddListener(() => SetQuestAsCurrent());
				}
			
				
				if (this.completed)
				{
					SetCurrentAsCompleted();
					SetButtons(false, false);


				}

			}
		
		}
	
	
	
		if (Input.GetKeyDown(KeyCode.Q))
		{
			bool active = (values.questBook.activeSelf) ? false : true;
			values.questBook.SetActive(active);
		}
		

	}
	
	public void AddToCoulomb()
	{
	
			quest = new GameObject(questName);
			text = quest.AddComponent<Text>();
			text.text = questName;
			text.color = Color.blue;
			text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			text.fontSize = 30;
			quest.transform.SetParent(coulomb.transform);
			quest.transform.localScale = new Vector3(1, 1, 1);
			text.alignment = TextAnchor.LowerCenter;
			btn = quest.AddComponent<Button>();
			accepted = true;
			HideQuest();
		
	}
	
	
	public void ShowQuest()
	{
		values.fullQuestView.SetActive(true);

		values.fullQuestViewIcon.sprite = SetQuestIcon();
		values.fullQuestViewContent.text = questContent;
	}
	public void AcceptQuest()
	{
		if(!accepted)
		{
			AddToCoulomb();
		}

		HideQuest();



	}
	
	public void SetQuestAsCurrent()
	{
		if (!completed)
		{
			if (!accepted)
			{
				AcceptQuest();
			}
			isSettedAsCurrent = true;
			values.currentQuestImage.color = new Color(1, 1, 1, 1);

			values.currentQuestImage.sprite = SetQuestIcon();
			values.currentQuestText.text = shortInfo;

			HideQuest();
			
			
		}
	}


	public void HideQuest()
	{
		values.fullQuestView.SetActive(false);
		values.fullQuestViewIcon.sprite = null;
		values.fullQuestViewContent.text = "";
		isChoosed = false;

	}






	private Sprite SetQuestIcon()
	{
		return questTypeIcon = values.icons[coulombIndex];
	}







	public void SetButtons(bool btnAccept, bool btnSetAsCurrent)
	{
		values.accept.gameObject.SetActive(btnAccept);
		values.close.gameObject.SetActive(true);
		values.setAsCurrent.gameObject.SetActive(btnSetAsCurrent);


	}

	




	public void SetCurrentAsCompleted()
	{
		coulomb = values.coulombs[4];
		quest.transform.SetParent(coulomb.transform);
		if (completed && isSettedAsCurrent)
		{
			if (manager.quests[manager.quests.IndexOf(this) + 1] != null&& manager.quests[manager.quests.IndexOf(this) + 1].accepted)
			{
				Quest nextQuest = manager.quests[manager.quests.IndexOf(this) + 1];
				
					nextQuest.SetQuestAsCurrent();

				
			}
			else
			{
				SetCurrentAsEmpty();
			}

		

		}
	
	}
	private void SetCurrentAsEmpty()
	{
	
			values.currentQuestImage.sprite = null;
			values.currentQuestImage.color = new Color(1, 1, 1, 0);
			values.currentQuestText.text = "";
		
	}





	private void OnTriggerStay(Collider other)
	{
	
		if (!accepted && other.gameObject.name == "QuestTrigger" && enable && Input.GetKeyDown(KeyCode.E))
		{
			
			Debug.Log("press E to open quest");
			this.isTrigged = true;

			SetButtons(true, true);


			ShowQuest();
			values.accept.onClick.AddListener(() => AcceptQuest());
			values.setAsCurrent.onClick.AddListener(() => SetQuestAsCurrent());
		}
		

	}
	private void OnTriggerExit(Collider other)
	{
		
		if (other.gameObject.name == "QuestTrigger" && enable)
		{
			this.isTrigged = false;
			HideQuest();

		}
	
	

	}
	private void showQuestIcon()
	{
		if (!accepted)
		{

			if (questIcon == null)
			{
				questIcon = new GameObject("Quest icon");
				questIcon.transform.SetParent(gameObject.transform);
				questIcon.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
				questIcon.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
				SpriteRenderer sr = questIcon.AddComponent<SpriteRenderer>();
				sr.sprite = SetQuestIcon();
			

			}
		}
		else { Destroy(questIcon); }
		


	}
}
