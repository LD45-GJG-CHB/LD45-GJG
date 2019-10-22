using System;
using UnityEngine;

public static class GameState
{
    public static string PlayerName;
    public static Highscore PlayerLastHighscore;
    public static Difficulty Difficulty = Difficulty.MEDIUM;
    public static bool isBonusMaps = false;
    public static int Score = 0;
    public static TextFont Font = TextFont.FiraCode;
    public static Theme CurrentTheme => ThemeManager.GetCurrentTheme();
}