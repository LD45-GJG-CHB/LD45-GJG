using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NewMainMenu
{
    public class MenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        private void Awake()
        {
            SetButtonColors();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ButtonSelectedCallback();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ButtonDeselectedCallback();
        }

        public void OnSelect(BaseEventData eventData)
        {
            ButtonSelectedCallback();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            ButtonDeselectedCallback();
        }
        
        private void ButtonSelectedCallback()
        {
            GetComponent<TextMeshProUGUI>().fontStyle =
                FontStyles.Bold | FontStyles.Underline;
        }

        private void ButtonDeselectedCallback()
        {
            GetComponent<TextMeshProUGUI>().fontStyle =
                FontStyles.Normal;
        }
        
        private void SetButtonColors()
        {
            var button = gameObject.GetComponent<Button>();

            var colors = button.colors;
            colors.selectedColor = Color.gray;
            colors.highlightedColor = Color.gray;
            colors.pressedColor = Color.gray;

            button.colors = colors;
        }
        
    }
}