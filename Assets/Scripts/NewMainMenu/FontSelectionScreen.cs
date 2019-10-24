using System;
using NewMainMenu.Base;
using Themes;
using UnityEngine;

namespace NewMainMenu
{
    public class FontSelectionScreen : AbstractScreen<FontSelectionScreen>
    {
        public void OnFontSelected(string fontName)
        {
            ThemeManager.Instance.SetFont(fontName);
        }
    }
}
