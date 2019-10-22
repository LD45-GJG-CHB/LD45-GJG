using System;
using NewMainMenu.Base;
using UnityEngine;

namespace NewMainMenu
{
    public class FontSelectionScreen : AbstractScreen<FontSelectionScreen>
    {
        public void OnFontSelected(string fontName)
        {
            ThemeManager.SetFont(fontName);
        }
    }
}
