using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maps
{
    public static Dictionary<string, string> maps;

    public static int mapSizeX = 3;
    public static int mapSizeY = 2;

    static Maps()
    {
        maps = new Dictionary<string, string>();
        maps.Add("map_0", Map_0);
    }

    private static string Map_0 = "aaa" +
        "aaa";


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
//        var List<string> rows = input
    }
}
