using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
[Serializable]
public class QuestManager : MonoBehaviour {
	[HideInInspector]
	public List<Quest> quests;
	private int CurrentQuestIndex = 0;
	public QuestValues values;
	private void Start()
	{
		values = FindObjectOfType<QuestValues>();

		quests = new List<Quest>();
		for (int i = 0; i < GetComponents<Quest>().Length; i++)
		{
			quests.Add(GetComponents<Quest>()[i]);

		}
		

	}
	private void Update()
	{
		values.close.onClick.AddListener(()=>HideQuest());
		
		
	}
	/// <summary>
	/// active object with q params
	/// </summary>
	/// <param name="q"></param>
	public void ShowQuest(Quest q)
	{ 
		values.fullQuestView.SetActive(true);

		values.fullQuestViewIcon.sprite = SetQuestIcon(q);
		values.fullQuestViewContent.text = q.questContent;
	}
	public void AcceptQuest(Quest q)
	{
		AddToCoulomb(q);
		HideQuest();

		
	
	}
	/// <summary>
	/// sets the q as current quest
	/// </summary>
	/// <param name="q"></param>
	public void SetQuestAsCurrent(Quest q)
	{
		values.currentQuestImage.color = new Color(1, 1, 1, 1);

		values.currentQuestImage.sprite = SetQuestIcon(q);
		values.currentQuestText.text = q.shortInfo;
		if(!q.completed)
		{
			AddToCoulomb(q);
		}
		q.isSettedAsCurrent= true;
		HideQuest();
		foreach (Quest e in quests)
		{
			if (e != q)
			{
				e.isSettedAsCurrent = false;
			}
		}
	}
	public bool IsCurrent(Quest q)
	{
		return q == quests[CurrentQuestIndex];
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
	private Sprite SetQuestIcon(Quest q)
	{
		return q.questTypeIcon = values.icons[q.coulombIndex];
	}
	public void SetButtons(bool btnAccept,bool btnSetAsCurrent)
	{
		 values.accept.gameObject.SetActive(btnAccept);
		values.close.gameObject.SetActive(true);
		values.setAsCurrent.gameObject.SetActive(btnSetAsCurrent);
		

	}/// <summary>
	/// sets the coloumb to q 
	/// </summary>
	/// <param name="q"></param>
	private void AddToCoulomb(Quest q)
	{
		q.isChoosed = false;

		q.coulomb = values.coulombs[q.coulombIndex];
		q.AddToCoulomb();
	}
	public void SetToCompleted(Quest q)
	{

		q.coulomb = values.coulombs.Last();
		q.quest.transform.SetParent(values.coulombs.Last().transform);
	}
	public void SetCurrentAsCompleted(Quest q)
	{
		values.currentQuestImage.sprite = null;
		values.currentQuestImage.color = new Color(1, 1, 1, 0);
		if (q.completed&&q.isSettedAsCurrent)
		{
			

			values.currentQuestText.text = "Quest completed!";
		

		}
		if (q.completed && !q.isSettedAsCurrent)
		{
			values.currentQuestText.text = "";

		}
	}

}
