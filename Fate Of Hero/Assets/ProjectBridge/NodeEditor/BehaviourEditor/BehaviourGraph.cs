using System.Collections;
using System.Collections.Generic;
using NodeEditor;
using UnityEngine;
using FourGames;

namespace BehaviourEditor
{

	[CreateAssetMenu(menuName = "BehaviourEditor/Graph")]

	public class BehaviourGraph : NodeGraph
	{
		public EntityScript character;
		public BehaviourLifeCycle LiveCycle;


		public override BaseNode AddNode(DrawNode drawNode, float x, float y, string title)
		{
			BaseNode n = base.AddNode(drawNode, x, y, title);
			n.BehaviourGraph = this;
			return n;
		}
		protected override  void Awake()
		{
			
			base.Awake();
			if (!nodes.Exists(f => f.drawNode is EnterNode))
			{
#if UNITY_EDITOR
                enterNode = new BaseNode(BehaviourEditor.DrawNodes.EnterNode, 10, 200, "", GenerateId());
#endif
                nodes.Add(enterNode);
				enterNode.BehaviourGraph = this;

			}
			else
			{
				enterNode = nodes.Find(f => f.drawNode is EnterNode);
			}


		}
	}
}