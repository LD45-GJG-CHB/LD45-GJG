using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Just lives somewhere in the runtime and is omnipotent
public static class FontManager
{
    private const string FontBasePath = "Fonts & Materials";
    private static readonly Dictionary<TextFont, string> FontPaths = new Dictionary<TextFont, string>
    {
        {TextFont.FiraCode, "FiraMono-Regular SDF"},
        {TextFont.Dotty, "dotty SDF"},
        {TextFont.Joystix, "joystix monospace SDF"},
        {TextFont.ComicSans, "COMIC SDF"}
    };
    
    private static List<TMP_FontAsset> _fonts;
    
    static FontManager()
    {
        _fonts = LoadFonts();
    }
    
    private static List<TMP_FontAsset> LoadFonts()
        => Resources.LoadAll<TMP_FontAsset>(FontBasePath).ToList();

    public static IEnumerable<TMP_FontAsset> GetAllFonts()
        => new List<TMP_FontAsset>(_fonts);

    public static TMP_FontAsset GetFont(string fontName)
        =>
            _fonts
                .FirstOrDefault(font => font.name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase));

    public static TMP_FontAsset GetMatchingFontAsset(this TextFont type)
    {
        if (FontPaths.TryGetValue(type, out var fontFullName))
        {
            return GetFont(fontFullName);
        }

        throw new ArgumentNullException(nameof(type), "Font not defined in font map");
    }

    public static void UpdateFont()
    {
        var font = GetMatchingFontAsset(GameState.Font);

        if (!font)
            return;
            
        var texts = (TextMeshProUGUI[]) Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI));
        foreach (var textMeshProUgui in texts)
        {
            textMeshProUgui.font = font;
        }
    }
}