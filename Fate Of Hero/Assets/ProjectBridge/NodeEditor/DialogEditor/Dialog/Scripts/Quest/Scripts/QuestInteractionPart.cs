
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/InteractionPart", fileName = "NewInteractionPart")]
public class QuestInteractionPart : QuestInteractPart,IID
{

	[SerializeField]
	private int targetId;
	public int ID
	{
		get
		{
			return targetId;
		}
	}

	public bool HaveSameID(IID id)
	{
		return id.ID == ID;
	}

	public override bool Completed()
	{
		if(currentValue >= totalValue)
		{
		
			return true;
		}
		
		return false;
	}
	public void Interact()
	{
		currentValue++;
	}
}
