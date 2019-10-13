using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Components
{
    [RequireComponent(typeof(Slider))]
    public class ButtonSlider : MonoBehaviour
    {
        [SerializeField]
        private Button plus, minus;
        [SerializeField]
        private TextMeshProUGUI display;
        private Slider slider;

        private bool initialized = false;

        void Awake()
        {
            Init();
        }
        public void SetSlider(bool inteegerNumbers, float min, float max, float startValue)
        {
            Init();
            slider.value = startValue;
            slider.maxValue = max;
            slider.minValue = min;
            slider.wholeNumbers = inteegerNumbers;
        }
        public float GetValue()
        {
            return slider.value;
        }
        private void Init()
        {
            if (!initialized)
            {
                slider = GetComponent<Slider>();
                if (display != null)
                    slider.onValueChanged.AddListener((float f) => display.text = f.ToString());
                if (plus != null)
                    plus.onClick.AddListener(() => slider.value += 1);
                if (minus != null)
                    minus.onClick.AddListener(() => slider.value -= 1);
                initialized = true;
            }
        }
    }
}