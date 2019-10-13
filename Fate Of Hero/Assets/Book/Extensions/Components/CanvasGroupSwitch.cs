using System;
using Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    [RequireComponent(typeof(Button))]
    public class CanvasGroupSwitch : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup target;

        [SerializeField]
        private bool on = true, off, withRaycast;

        private Button btn;

        public CanvasGroup Target => target;

        void Awake()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(() => Handle());
        }
        public void Handle()
        {
            if (IsActive())
            {
                if (off)
                    Deactive();
            }
            else
            {
                if (on)
                    Active();
            }
        }
        public void Active()
        {
            if (!IsActive())
            {
                target.Active(withRaycast);
            }
        }
        public void Deactive()
        {
            if (IsActive())
            {
                target.Deactive(withRaycast);
            }
        }
        public bool IsActive()
        {
            return target.IsActive(withRaycast);
        }

        public void Disable()
        {
            btn.interactable = false;
        }
        public void Enable()
        {
            btn.interactable = true;
        }

    }
}