using System;
using Themes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ThemeManager
{
    public enum PredefinedThemes
    {
        BLACK,
        WHITE,
        SAFFRON,
        CITRUS,
        OCEAN
    }

    private static Theme _currentTheme = GetTheme(PredefinedThemes.BLACK);
    
    
    public static void SetCurrentTheme(Theme theme)
    {
        _currentTheme = theme;
    }

    public static void SetCurrentTheme(PredefinedThemes themeEnum)
    {
        SetCurrentTheme(GetTheme(themeEnum));
    }
    
    public static void SetCurrentTheme(String theme)
    {
        SetCurrentTheme(getTheme(theme));
    }

    public static Theme GetCurrentTheme()
    {
        return _currentTheme;
    }

    public static Theme GetTheme(PredefinedThemes themeEnum)
    {
        switch (themeEnum)
        {
            case PredefinedThemes.WHITE:
                return new WhiteTheme();

            case PredefinedThemes.SAFFRON:
                return new SaffronTheme();
            case PredefinedThemes.CITRUS:
                return new CitrusTheme();
            case PredefinedThemes.OCEAN:
                return new OceanTheme();
            case PredefinedThemes.BLACK:
            default:
                return new BlackTheme();
        }
    }

    public static Theme getTheme(String theme)
    {
        switch (theme)
        {
            case "saffron": return new SaffronTheme();
            case "ocean": return new OceanTheme();
            case "citrus": return new CitrusTheme();
            case "white": return new WhiteTheme();
            case "black":
            default:
                return new BlackTheme();
            
        }
    }
}
