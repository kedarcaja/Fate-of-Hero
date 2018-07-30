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
	
	private GameObject quest;
	private Button btn;
	private QuestManager manager;
	
	private void Start()
	{
		manager = GetComponent<QuestManager>();
		coulombIndex = (int)(questType);// getting int value from enum
		values = FindObjectOfType<QuestValues>();
		coulomb = values.coulombs[coulombIndex];
	}
	
	private void Update()
	{
		values.close.onClick.AddListener(()=>HideQuest());
		
		if (enable)
		{
			if (this.isTrigged&&!this.accepted)
			{
				
					ShowQuest();
					SetButtons(true,true);
				values.accept.onClick.AddListener(() => AcceptQuest());
				values.setAsCurrent.onClick.AddListener(() => SetQuestAsCurrent());


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
				if(this.completed&&this.isChoosed)
				{
					SetButtons(false,false);
				}
				if (this.completed)
				{
					SetToCompleted();
					SetCurrentAsCompleted();

				}

			}
		
		}
	
		#region Inputs
	
		if (Input.GetKeyDown(KeyCode.Q))
		{
			bool active = (values.questBook.activeSelf) ? false : true;
			values.questBook.SetActive(active);
		}
		
		#endregion
	}
	
	public void AddToCoulomb()
	{
		if (!accepted)
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
		AddToCoulomb();
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
	
	/// <summary>
	/// close quest list view
	/// </summary>
	public void HideQuest()
	{
		values.fullQuestView.SetActive(false);
		values.fullQuestViewIcon.sprite = null;
		values.fullQuestViewContent.text = "";

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
	
	public void SetToCompleted()
	{

		coulomb = values.coulombs[4];
		quest.transform.SetParent(coulomb.transform);
	}
	public void SetCurrentAsCompleted()
	{
		values.currentQuestImage.sprite = null;
		values.currentQuestImage.color = new Color(1, 1, 1, 0);
		if (completed && isSettedAsCurrent)
		{


			values.currentQuestText.text = "Quest completed!";


		}
		if (completed && !isSettedAsCurrent)
		{
			values.currentQuestText.text = "";

		}
	}
	private void OnTriggerStay(Collider other)//stay->bug?Enter-> correct
	{

		if (!accepted && other.gameObject.name == "QuestTrigger" && this.enable)
		{
			this.isTrigged = true;

			SetButtons(true, true);


			ShowQuest();
		}
		

	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "QuestTrigger" && enable)
		{
			this.isTrigged = false;
			HideQuest();

		}
		if (other.gameObject.name == "QuestTrigger" && completed)
		{
			SetCurrentAsCompleted();
			isSettedAsCurrent = false;
		}

	}

}
