using UnityEngine;

namespace Themes
{
    [CreateAssetMenu(fileName = "New Theme", menuName = "Theme/New Theme", order = 1)]
    public class Theme : ScriptableObject
    {
        public Color backgroundColor = Color.black;
        public Color fontColor = Color.white;
        public Color playerColor = Color.red;
        public float inactiveFontAlpha = .3f;
        public ThemeType themeType = ThemeType.BLACK;
    }

    public enum ThemeType
    {
        BLACK,
        WHITE,
        CORAL,
        SUNSET,
        TULIP,
        MATRIX
    }
}