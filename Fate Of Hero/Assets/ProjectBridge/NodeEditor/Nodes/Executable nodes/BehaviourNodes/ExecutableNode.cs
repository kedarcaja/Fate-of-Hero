using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeEditor {
	public abstract class ExecutableNode : DrawNode
	{
		public abstract void Execute(BaseNode b);
	}
}