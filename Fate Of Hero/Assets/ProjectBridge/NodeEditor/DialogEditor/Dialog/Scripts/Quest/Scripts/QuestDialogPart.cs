using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName = "Quest/DialogPart", fileName = "NewDialogPart")]
public class QuestDialogPart : QuestPart
{
	 [SerializeField]
	// private Dialog dialog;
	 
	 
	public override bool Completed()
	{
		//if (dialog.WasPlayed)
		{
			if (quest.Completed)
			{
				QuestManager.Instance.SetQuestAsCompleted(quest);
			}
			return true;

		}
		return false;
			

	}
	
	 
	
}
