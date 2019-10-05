using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class Maps
{
    public static Dictionary<string, string> maps;

    public static int mapSizeX = 40;
    public static int mapSizeY = 20;

    private static string path = "Assets/Resources/Maps/";

    static Maps()
    {
        maps = new Dictionary<string, string>();
        maps.Add("map_0", Map_0());
    }

    private static string Map_0()
    {
        StreamReader reader = new StreamReader(path + "map_0.txt");
        string map = reader.ReadToEnd().Replace(System.Environment.NewLine, "");
        reader.Close();
        return map;
    }

    public static string[,] StringTo2DArray(string input)
    {
        const int n = 3;
        var split = input
            .Select((c, i) => new { letter = c, group = i / n })
            .GroupBy(l => l.group, l => l.letter)
            .Select(g => string.Join("", g))
            .ToArray();

        var result = new string[mapSizeX,mapSizeY];
        for (int yIndex = 0; yIndex < mapSizeY; yIndex++)
        {
            for (int xIndex = 0; xIndex < mapSizeX; xIndex++)
            {
                result[xIndex, yIndex] = split[yIndex][xIndex].ToString();
            }
        }
        
        return result;
    }
}
