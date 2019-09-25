using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NodeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/EditorSettings")]
    public class EditorSettings : ScriptableObject
    {
        #region Style
        public float gridSpacing = 10, gridOpacity = 0.2f;
        public Color backgroundColor = new Color32(37, 37, 37, 255), gridColor = Color.gray, otherGUIColor = new Color32(50, 50, 50, 255);
		#endregion
	}
}