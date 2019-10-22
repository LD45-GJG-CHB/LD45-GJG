using Menus;
using NewMainMenu.Base;
using UnityEngine;

namespace NewMainMenu
{
    public class ThemeSelectionScreen : AbstractScreen<ThemeSelectionScreen>
    {
        public void OnThemeSelected(string theme)
        {
            ThemeManager.SetCurrentTheme(theme);
            ThemeUpdater.Instance.UpdateTheme();
        }
    }
}
