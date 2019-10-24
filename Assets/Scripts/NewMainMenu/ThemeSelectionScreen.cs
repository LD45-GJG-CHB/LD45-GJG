using System;
using NewMainMenu.Base;
using Themes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NewMainMenu
{
    public class ThemeSelectionScreen : AbstractScreen<ThemeSelectionScreen>
    {
        [SerializeField] private GameObject listItemPrefab;
        [SerializeField] private GameObject itemList;
        
        private void Start()
        {
            FillThemeList();
        }

        private void FillThemeList()
        {
            var themes = ThemeManager.Instance.GetAllThemes();

            foreach (var theme in themes)
            {
                var go = Instantiate(listItemPrefab, itemList.transform);
                go.GetComponent<TextMeshProUGUI>().SetText(theme.name);
                go.GetComponent<Button>().onClick.AddListener(() => OnThemeSelected(theme.name));
                go.transform.SetAsFirstSibling();
            }
        }
        
        private static void OnThemeSelected(string theme)
        {
            ThemeManager.Instance.SetCurrentTheme(theme);
            ThemeManager.Instance.UpdateTheme();
        }
    }
}
