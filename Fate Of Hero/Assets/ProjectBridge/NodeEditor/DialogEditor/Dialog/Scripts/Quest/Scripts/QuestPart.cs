using UnityEngine;
using UnityEngine.Events;
public abstract class QuestPart : ScriptableObject
{
	[SerializeField]
	protected Quest quest;
	public Quest Quest { get { return quest; } }
	[TextArea(5, 10)]
	[SerializeField]
	protected string description;
	[SerializeField]
	protected bool compulsory;
	public bool Compulsory { get { return compulsory; } }
	public string Description
	{
		get
		{
			return description;
		}
	}
	public UnityEvent OnComplete;
	public abstract bool Completed();
}