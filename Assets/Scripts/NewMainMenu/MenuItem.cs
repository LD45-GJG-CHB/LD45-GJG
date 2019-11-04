using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Screen = NewMainMenu.Base.Screen;

namespace NewMainMenu
{
    public class MenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        private bool _selected;
        private EventSystem _eventSystem;

        private void Awake()
        {
            _eventSystem = EventSystem.current;
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
            if (_selected)
                return;

            _selected = true;

            var selectable = GetComponent<Selectable>();
            GetComponentInParent<Screen>().lastSelected = selectable;
            selectable.Select();
            
            SetSelectedStyle();
        }

        private void ButtonDeselectedCallback()
        {
            if (!_selected) 
                return;

            if(!_eventSystem.alreadySelecting)
                _eventSystem.SetSelectedGameObject(null);
            
            _selected = false;
                
            SetNormalStyle();
        }

        private void SetNormalStyle()
        {
            GetComponent<TextMeshProUGUI>().fontStyle =
                FontStyles.Normal;
        }

        private void SetSelectedStyle()
        {
            GetComponent<TextMeshProUGUI>().fontStyle =
                FontStyles.Bold | FontStyles.Underline;
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