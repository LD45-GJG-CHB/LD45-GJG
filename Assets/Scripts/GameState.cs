using System;
using UnityEngine;

public static class GameState
{
    public static bool IsPlayerDead;
    public static String playerName;
    public static Highscore playerLastHighscore;
    public static Difficulty Difficulty = Difficulty.MEDIUM;

    public static TextFont Font;
    
    public static Color BackgroundColor;
    public static Color TextColor;
}