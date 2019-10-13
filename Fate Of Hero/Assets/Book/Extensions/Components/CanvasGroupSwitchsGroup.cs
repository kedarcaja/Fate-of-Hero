using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.Components
{
    public class CanvasGroupSwitchsGroup : MonoBehaviour
    {
        [SerializeField]
        private bool switchesAreChilds = false;
        [SerializeField]
        private List<CanvasGroupSwitch> switchs = new List<CanvasGroupSwitch>();
        [SerializeField]
        private  CanvasGroupSwitch defaul;
        private CanvasGroupSwitch current;
        public CanvasGroupSwitch Current => current;
        public List<CanvasGroupSwitch> Switchs => switchs;

        public CanvasGroupSwitch Defaul => defaul;

        void Awake()
        {
            if (switchesAreChilds)
            {
                switchs.AddRange(transform.GetComponentsInChildren<CanvasGroupSwitch>());
            }
            foreach (CanvasGroupSwitch s in switchs)
            {
                s.GetComponent<Button>().onClick.AddListener(() => { Check(s); });
            }
        }
        void Start()
        {
            if (Defaul != null)
                Check(Defaul);
        }

        public void Check(CanvasGroupSwitch selected)
        {
            switchs.ForEach(f => f.Deactive());
           
            selected.Active();
            selected.Enable();
           
            if (selected)
                current = selected;
        }
        public void EnableAll()
        {
            switchs.ForEach(f => f.Enable());

        }
        public void DisableAll()
        {
            switchs.ForEach(f => f.Disable());
        }

    }
}