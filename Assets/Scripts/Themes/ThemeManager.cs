using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Themes
{
    public class ThemeManager : Singleton<ThemeManager>
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Material panelMaterial;
        [SerializeField] private Material playerMaterial;

        private static readonly int Color = Shader.PropertyToID("_Color");
        private static readonly string ThemesFolder = Path.Combine("DataObjects", "Themes");

        private List<Theme> _themes;
        
        private Theme _currentTheme;

        public Theme CurrentTheme
        {
            get =>
                _currentTheme 
                    ? _currentTheme 
                    : GetTheme(ThemeType.BLACK);
            
            private set => _currentTheme = value;
        }

        private void Awake()
        {
            _themes = LoadThemes();
            DontDestroyOnLoad(Instance);
        }

        private static List<Theme> LoadThemes()
            => Resources.LoadAll<Theme>(ThemesFolder).ToList();

        public void UpdateTheme()
        {
            // Update font
            UpdateFont();
            // Update player material
            playerMaterial.SetColor(Color, CurrentTheme.playerColor);
            // Update panel material
            panelMaterial.SetColor(Color, CurrentTheme.backgroundColor);
            // Update text color
            text.fontSharedMaterial.SetColor(ShaderUtilities.ID_FaceColor, CurrentTheme.fontColor);
        }

        private static void UpdateFont()
        {
            const string fontBasePath = "Fonts & Materials";

            var fontPaths = new Dictionary<TextFont, string>
            {
                {TextFont.FiraCode, "FiraMono-Regular SDF"},
                {TextFont.Dotty, "dotty SDF"},
                {TextFont.Joystix, "joystix monospace SDF"},
                {TextFont.ComicSans, "COMIC SDF"}
            };

            if (fontPaths.TryGetValue(GameState.Font, out var path))
            {
                var font = Resources.Load<TMP_FontAsset>(Path.Combine(fontBasePath, path));

                var texts = (TextMeshProUGUI[]) Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI));
                foreach (var textMeshProUgui in texts)
                {
                    textMeshProUgui.font = font;
                }
            }
            else
            {
                Debug.LogError($"Font {GameState.Font} not defined");
            }
        }

        public Theme GetTheme(string themeName)
            =>
                _themes
                    .FirstOrDefault(theme => theme.name.Equals(themeName, StringComparison.InvariantCultureIgnoreCase));

        public Theme GetTheme(ThemeType type)
            =>
                _themes
                    .FirstOrDefault(theme => theme.themeType == type);

        public IEnumerable<Theme> GetAllThemes() 
            => new List<Theme>(_themes);

        public void SetCurrentTheme(string newTheme)
            => CurrentTheme = GetTheme(newTheme);
        
        public void SetCurrentTheme(ThemeType type)
            => CurrentTheme = GetTheme(type);

        public void SetFont(string fontName)
        {
            switch (fontName.Trim().ToLowerInvariant())
            {
                case "comicsans":
                    GameState.Font = TextFont.ComicSans;
                    break;
                case "dotty":
                    GameState.Font = TextFont.Dotty;
                    break;
                case "joystix":
                    GameState.Font = TextFont.Joystix;
                    break;
                case "firacode":
                    GameState.Font = TextFont.FiraCode;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Font not defined in script {nameof(GetType)} but is added to the screen.");
            }
        
            UpdateTheme();
        }

    }
}