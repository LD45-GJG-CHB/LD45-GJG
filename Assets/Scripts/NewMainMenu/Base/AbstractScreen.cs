using System.Linq;
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

        protected override void Awake()
        {
            base.Awake();
            AddMenuItemComponents();
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                SelectFirstSelectableInChildren(Instance.gameObject);
            }
        }

        private void OnEnable()
        {
            SelectFirstSelectableInChildren(Instance.gameObject);
        }

        private static void SelectFirstSelectableInChildren(GameObject screen)
        {
            var firstSelectable = screen.GetComponentInChildren<Selectable>();
            // Select the button
            firstSelectable.Select();
            // Highlight the button
            firstSelectable.OnSelect(null);
        }

        protected override void OnDestroy()
        {
            RemoveMenuItemComponents();
            base.OnDestroy();
        }

        private static void AddMenuItemComponents()
        {
            // TODO: Disallow having 2 objects selected at the same time via mouse/keyboard selection
            Instance.GetComponentsInChildren<Button>()
                .Select(button => button.gameObject)
                .ToList()
                .ForEach(go => go.AddComponent<MenuItem>());
        }

        private static void RemoveMenuItemComponents()
        {
            Instance.GetComponentsInChildren<MenuItem>()
                .ToList()
                .ForEach(Destroy);
        }
        
    }
}