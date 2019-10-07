using System;
using UnityEngine;

public static class GameState
{
    public static bool IsPlayerDead;
    public static String playerName;
    public static Highscore playerLastHighscore;
    public static Difficulty Difficulty = Difficulty.MEDIUM;
    public static bool isBonusMaps = false;
    public static int score = 0;

    public static TextFont Font;

//    public static Color BackgroundColor => ThemeManager.getCurrentTheme().BackgroundColor;
//    public static Color TextColor => ThemeManager.getCurrentTheme().FontColor;
    public static Theme currentTheme => ThemeManager.GetCurrentTheme();
}