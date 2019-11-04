using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Themes
{
    // TODO: This font stuff should have nothing to do with themes D:
    public class ThemeManager : Singleton<ThemeManager>
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Material panelMaterial;
        [SerializeField] private Material playerMaterial;

        private static readonly int Color = Shader.PropertyToID("_Color");
        private static readonly string ThemesFolder = Path.Combine("DataObjects", "Themes");


        private const string FontBasePath = "Fonts & Materials";

        private static readonly Dictionary<TextFont, string> FontPaths = new Dictionary<TextFont, string>
        {
            {TextFont.FiraCode, "FiraMono-Regular SDF"},
            {TextFont.Dotty, "dotty SDF"},
            {TextFont.Joystix, "joystix monospace SDF"},
            {TextFont.ComicSans, "COMIC SDF"}
        };


        private List<Theme> _themes;
        private List<TMP_FontAsset> _fonts;

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
            _fonts = LoadFonts();
            DontDestroyOnLoad(Instance);
        }

        private static List<Theme> LoadThemes()
            => Resources.LoadAll<Theme>(ThemesFolder).ToList();

        private static List<TMP_FontAsset> LoadFonts()
            => Resources.LoadAll<TMP_FontAsset>(FontBasePath).ToList();

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

        private void UpdateFont()
        {
            var font = GetFont(GameState.Font);

            if (!font)
                return;
            
            var texts = (TextMeshProUGUI[]) Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI));
            foreach (var textMeshProUgui in texts)
            {
                textMeshProUgui.font = font;
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

        public IEnumerable<TMP_FontAsset> GetAllFonts()
            => new List<TMP_FontAsset>(_fonts);

        public TMP_FontAsset GetFont(string fontName)
            =>
                _fonts
                    .FirstOrDefault(font => font.name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase));

        public TMP_FontAsset GetFont(TextFont type)
        {
            if (FontPaths.TryGetValue(type, out var fontFullName))
            {
                return _fonts
                    .FirstOrDefault(font =>
                        font.name.Equals(fontFullName, StringComparison.InvariantCultureIgnoreCase));
            }

            throw new ArgumentNullException(nameof(type), "Font not defined in font map");
        }

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
                    throw new ArgumentOutOfRangeException(
                        $"Font not defined in script {nameof(GetType)} but is added to the screen.");
            }

            UpdateTheme();
        }
    }
}