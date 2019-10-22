using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Menus
{
    public class ThemeUpdater : Singleton<ThemeUpdater>
    {
        public TextMeshProUGUI TextComponent;
        public Material PanelMaterial;
        public Material PlayerMaterial;
        
        private static readonly int Color = Shader.PropertyToID("_Color");

        // Start is called before the first frame update
        void Start()
        {
        }

        private void Awake()
        {
            UpdateTheme();
        }

        public void UpdatePanelMaterialColor()
        {
        }


        public void UpdateTextColor()
        {
        }

        public void UpdateTheme()
        {
            // Update font
            UpdateFont();
            // Update player material
            PlayerMaterial.SetColor(Color, ThemeManager.GetCurrentTheme().FontColor);
            // Update panel material
            PanelMaterial.SetColor(Color, ThemeManager.GetCurrentTheme().BackgroundColor);
            // Update text color
            TextComponent.fontSharedMaterial.SetColor(ShaderUtilities.ID_FaceColor, ThemeManager.GetCurrentTheme().FontColor);
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

                foreach (var textMeshProUgui in (TextMeshProUGUI[]) FindObjectsOfTypeAll(typeof(TextMeshProUGUI)))
                {
                    textMeshProUgui.font = font;
                }
            }
            else
            {
                Debug.LogError($"Font {GameState.Font} not defined");
            }
        }
    }
}