using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName="Quest/CollectPart",fileName= "NewCollectPart")]
public class QuestCollectPart : QuestPart
{
	[SerializeField]
	private int totalValue;
	//[SerializeField]
	//private Item collectItem;

	//public Item CollectItem
	//{
	//	get
	//	{
	//		return collectItem;
	//	}

	
	//}

	public override bool Completed()
	{

		int val = 0;
	
		//foreach (BagScript b in Inventory.Instance.Bags)
		//{
		//	foreach (Slot s in b.Bag.Slots)
		//	{
		//		if (s.CurrentItem != CollectItem) continue;
		//		val += s.Items.Count();
		//	}
		//}
		if(val >= totalValue)
		{
			if (OnComplete != null)
			{
				OnComplete.Invoke();
			}
		

			return true;
		}
		return false;
	}
}
