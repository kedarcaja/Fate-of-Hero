using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuestCollectable : MonoBehaviour {
	[SerializeField]
private	QuestManager manager;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "QuestTrigger")
		{
			Destroy(gameObject);	
			foreach (Quest q in manager.quests)
			{
				if (q.isSettedAsCurrent)
				{
					q.questObjectCollected = true;
				}
			}
		}
	}
}
