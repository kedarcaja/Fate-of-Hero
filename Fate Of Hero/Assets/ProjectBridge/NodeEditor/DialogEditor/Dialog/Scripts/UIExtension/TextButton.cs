using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Unity.UIExtension {
    public class TextButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]
        private Color normal = Color.black, focused = Color.black;
        private TextMeshProUGUI text;
        public UnityEvent _OnPointerClick, _OnPointerEnter, _OnPointerExit, _OnPointerUp, _OnPointerDown;
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Focus();
            _OnPointerClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Focus();
            _OnPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RemoveFocus();
            _OnPointerExit?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            RemoveFocus();
            _OnPointerUp?.Invoke();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Focus();
            _OnPointerDown?.Invoke();
        }
        private void Focus()
        {
            text.color = focused;
        }
        private void RemoveFocus()
        {
            text.color = normal;
        }

        public void SetColors(Color normal, Color focused)
        {
            this.normal = normal;
            this.focused = focused;
        }
        public void SetText(string text)
        {
            this.text.text = text;
        }
        public void Deactive()
        {
            gameObject.SetActive(false);
        }
        public void Active()
        {
            gameObject.SetActive(true);
        }
    }
}
