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
	private void Start()
	{
		

		quests = new List<Quest>();
		for (int i = 0; i < GetComponents<Quest>().Length; i++)
		{
			quests.Add(GetComponents<Quest>()[i]);

		}
		

	}
	
	

}
