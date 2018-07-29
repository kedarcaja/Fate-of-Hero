using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class QuestValues : MonoBehaviour {
	
	[ExecuteInEditMode]
	public List<GameObject> coulombs;
	[Space]
	[Space]
	[ExecuteInEditMode]
	public List<Sprite> icons;
	[Space]
	[Space]
	[Header("full quest info")]
	public GameObject fullQuestView, questBook;
[ExecuteInEditMode]
	[Header("full quest info")]
	public Text fullQuestViewContent;
	[Header("full quest info")]
	[ExecuteInEditMode]
	public Image fullQuestViewIcon;
	[Space]
	[Space]
	[Header("current quest")]
	public Text currentQuestText;
	[Header("current quest")]
	public Image currentQuestImage;
	[Space]
	[Space]
	[Header("Buttons")]
	public Button accept, close, setAsCurrent;

}
