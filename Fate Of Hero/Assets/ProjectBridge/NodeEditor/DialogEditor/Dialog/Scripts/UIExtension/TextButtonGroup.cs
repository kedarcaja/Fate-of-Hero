using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.UIExtension
{
    public class TextButtonGroup : MonoBehaviour
    {
        [SerializeField]
        private bool buttonsAreChilds = false;
        [SerializeField]
        private List<TextButton> buttons = new List<TextButton>();
        [SerializeField]
        private Color focusColor = Color.red, normalColor = Color.black;
        public UnityEvent OnButtonSelect;


        public int lastSelectedButtonIndex;
        private void Awake()
        {
            lastSelectedButtonIndex = int.MaxValue;
            if (buttonsAreChilds)
            {
                buttons.AddRange(transform.GetComponentsInChildren<TextButton>());
            }

            buttons.ForEach(b => { b._OnPointerClick.AddListener(() => { lastSelectedButtonIndex = buttons.IndexOf(b); OnButtonSelect?.Invoke(); }); b.SetColors(normalColor, focusColor); });

        }
        public void ActiveButtons(int count)
        {
            if (buttons.Count == 0) return;

            DeactiveButtons();
            for (int i = 0; i < count; i++)
            {
                buttons[i].Active();
            }
        }
        public void DeactiveButtons()
        {
            if (buttons.Count == 0) return;

            buttons.ForEach(b => b.Deactive());
        }

        public void SetGroup(string[] data)
        {
            ActiveButtons(data.Length);

            for (int i = 0; i < data.Length; i++)
            {
               buttons[i].SetText(data[i]);
            }
        }
    }
}
