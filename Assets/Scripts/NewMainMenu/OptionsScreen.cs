using NewMainMenu.Base;
using UnityEngine;

namespace NewMainMenu
{
    public class OptionsScreen : AbstractScreen<OptionsScreen>
    {
        public void OnThemePressed()
        {
            ThemeSelectionScreen.Show();
        }

        public void OnFontPressed()
        {
            FontSelectionScreen.Show();
        }
    }
}
