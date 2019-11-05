using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public static class DifficultyManager
{
    private static readonly string DifficultyBasePath = Path.Combine("DataObjects", "Difficulties");
    private static readonly List<Difficulty> Difficulties;

    static DifficultyManager()
    {
        Difficulties = LoadFonts();
    }
    
    private static List<Difficulty> LoadFonts()
        => Resources.LoadAll<Difficulty>(DifficultyBasePath).ToList();

    public static IEnumerable<Difficulty> GetAllDifficulties()
        => new List<Difficulty>(Difficulties);

    public static Difficulty GetDifficulty(string fontName)
        =>
            Difficulties.FirstOrDefault(font => font.name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase));

    public static Difficulty GetMatchingDifficulty(this DifficultyLevel level) 
        => Difficulties.FirstOrDefault(diff => diff.difficultyLevel == level);
}