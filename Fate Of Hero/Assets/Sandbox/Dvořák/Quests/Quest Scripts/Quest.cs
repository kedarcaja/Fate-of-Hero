using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour {
	public  enum EQuestType { Main = 0 , Other = 1, Profesion = 2, Aktivity = 3, Completed = 4 }
	public bool isAftedDialog;
	public Subtitles dialog;
	public EQuestType questType;
	[HideInInspector]
	public GameObject coulomb;
	[HideInInspector]
	public int coulombIndex;
	public string questName;
	public bool  questObjectCollected;
	[TextAreaAttribute(1, 1)]
	public string shortInfo;
	[HideInInspector]
	public Text text;
	[HideInInspector]
	public Sprite questTypeIcon;

	[TextAreaAttribute(12,2000)]
	public string questContent;
	public bool completed, accepted, added, enable, isChoosed,isTrigged,isSettedAsCurrent;
	
	public GameObject quest;
	[HideInInspector]
	public Button btn;
	private QuestManager manager;
	
	private void Start()
	{
		manager = GetComponent<QuestManager>();
		coulombIndex = (int)(questType);// getting int value from enum
	
	}
	
	private void Update()
	{

		if (StartQuestAfterDialog())
		{
			enable = true;
		}
		if (enable)
		{
			if (this.isTrigged&&!this.added)
			{
				
					manager.ShowQuest(this);
					manager.SetButtons(true,true);
				manager.values.accept.onClick.AddListener(() => manager.AcceptQuest(this));
				manager.values.setAsCurrent.onClick.AddListener(() => manager.SetQuestAsCurrent(this));


			}
			if (this.added)
			{
				btn.onClick.AddListener(() => manager.ShowQuest(this));
				btn.onClick.AddListener(() => this.isChoosed = true);
				if (isChoosed&&!completed)
				{

					manager.SetButtons(false, true);
					manager.values.setAsCurrent.onClick.AddListener(() => manager.SetQuestAsCurrent(this));
				}
				if(this.completed&&this.isChoosed)
				{
					manager.SetButtons(false,false);
				}
				if (this.completed)
				{
					manager.SetToCompleted(this);
					manager.SetCurrentAsCompleted(this);

				}

			}
		
		}
	
		#region Inputs
	
		if (Input.GetKeyDown(KeyCode.Q))
		{
			bool active = (manager.values.questBook.activeSelf) ? false : true;
			manager.values.questBook.SetActive(active);
		}
		
		#endregion
	}
	/// <summary>
	/// creacte object which will be added to quest list
	/// </summary>
	public void AddToCoulomb()
	{
		if (!added)
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
			added = true;
		}
	}
	private void OnTriggerStay(Collider other)//stay->bug?Enter-> correct
	{

		if (!accepted&&other.gameObject.name=="QuestTrigger"&&this.enable)
		{
			this.isTrigged = true;
		
				manager.SetButtons(true,true);
				this.accepted = true;

			
			manager.ShowQuest(this);
		}
		if (other.gameObject.name == "QuestTrigger"&&isSettedAsCurrent&&questObjectCollected)
		{
			completed = true;
			
		}
		
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "QuestTrigger"&&enable)
		{
			this.isTrigged = false;
			manager.HideQuest();

		}
		if (other.gameObject.name == "QuestTrigger"&&completed)
		{
			isSettedAsCurrent = false;
			manager.SetCurrentAsCompleted(this);
		}
		
	}
	private bool StartQuestAfterDialog()
	{
		return dialog.Dialogs[0].ended && isAftedDialog;
	}
}
