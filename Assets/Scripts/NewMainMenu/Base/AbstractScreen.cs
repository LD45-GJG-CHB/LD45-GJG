using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NewMainMenu.Base
{
    /// <summary>A base screen class that implements parameterless Show and Hide methods</summary>
    public abstract class AbstractScreen<T> : Screen<T> where T : AbstractScreen<T>
    {
        public static void Show() => Open();

        public static void Hide() => Close();

        private void OnEnable()
        {
            SetButtonStyles();
            SelectFirstSelectableInChildren(Instance.gameObject);
        }
        
        private void OnDisable()
        {
            RemoveButtonEventTriggers();
        }

        private static void SelectFirstSelectableInChildren(GameObject screen)
        {
            var firstSelectable = screen.GetComponentInChildren<Selectable>();
            // Select the button
            firstSelectable.Select();
            // Highlight the button
            firstSelectable.OnSelect(null);
        }

        private static void RemoveButtonEventTriggers()
        {
            Instance.GetComponentsInChildren<Button>(true)
                .ToList()
                .ForEach(button => Destroy(button.gameObject.GetComponent<EventTrigger>()));
        }
        
        private void AddButtonEventTriggers(GameObject go)
        {
            var eventTrigger = go.gameObject.AddComponent<EventTrigger>();
            var onSelectEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Select,
                callback = new EventTrigger.TriggerEvent()
            };
            onSelectEntry.callback.AddListener(ButtonSelectedCallback);
            eventTrigger.triggers.Add(onSelectEntry);

            var onDeselectEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Deselect,
                callback = new EventTrigger.TriggerEvent()
            };
            onDeselectEntry.callback.AddListener(ButtonDeselectedCallback);
            eventTrigger.triggers.Add(onDeselectEntry);
        }
        
        private void SetButtonStyles()
        {
            // TODO: Disallow having 2 objects selected at the same time via mouse/keyboard selection
            Instance.GetComponentsInChildren<Button>()
                .Select(button => button.gameObject)
                .ToList()
                .ForEach(go =>
                {
                    SetButtonColors(go);
                    AddButtonEventTriggers(go);
                });
        }

        private static void SetButtonColors(GameObject go)
        {
            var button = go.GetComponent<Button>();

            var colors = button.colors;
            colors.selectedColor = Color.gray;
            colors.highlightedColor = Color.gray;
            colors.pressedColor = Color.gray;

            button.colors = colors;
        }

        private static void ButtonSelectedCallback(BaseEventData eventData)
        {
            eventData.selectedObject.GetComponent<TextMeshProUGUI>().fontStyle =
                FontStyles.Bold | FontStyles.Underline;
        }

        private static void ButtonDeselectedCallback(BaseEventData eventData)
        {
            eventData.selectedObject.GetComponent<TextMeshProUGUI>().fontStyle =
                FontStyles.Normal;
        }
    }
}