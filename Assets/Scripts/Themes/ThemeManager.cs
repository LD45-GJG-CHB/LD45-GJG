public static class ThemeManager
{
    public enum PredefinedThemes
    {
        BLACK,
        WHITE
    }

    private static Theme _currentTheme = getTheme(PredefinedThemes.BLACK);
    
    
    public static void setCurrentTheme(Theme theme)
    {
        _currentTheme = theme;
    }

    public static void setCurrentTheme(PredefinedThemes themeEnum)
    {
        _currentTheme = getTheme(themeEnum);
    }

    public static Theme getCurrentTheme()
    {
        return _currentTheme;
    }

    public static Theme getTheme(PredefinedThemes themeEnum)
    {
        switch (themeEnum)
        {
            case PredefinedThemes.WHITE:
                return new WhiteTheme();
            
            case PredefinedThemes.BLACK:
            default:
                return new BlackTheme();
        }
    }
}
