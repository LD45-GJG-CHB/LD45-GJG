using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Maps
{
    public static List<string> mapNames;

    public static Dictionary<string, string> maps;

    public static int mapSizeX = 40;
    public static int mapSizeY = 20;

    private static string path = "Assets/Resources/Maps/";

    static Maps()
    {
        mapNames = createOrderedMapNames();
        maps = new Dictionary<string, string>();
        foreach (string mapName in mapNames)
        {
            maps.Add(mapName, ReadMap(mapName));
        }
    }

    private static List<string> createOrderedMapNames()
    {
        List<string> _mapNames = new List<string>();
        FileInfo[] files = new DirectoryInfo(path).GetFiles("map_*.txt");
        foreach (FileInfo file in files)
        {
            _mapNames.Add(file.Name);
        }
        _mapNames = _mapNames.OrderBy(o => MapNumber(o.Substring(4))).ToList();
        return _mapNames;
    }

    private static int MapNumber(string mapNumber)
    {
        try
        {
            return Int32.Parse(mapNumber.Replace(".txt", ""));
        }
        catch (FormatException e)
        {
            return int.MaxValue;
        }
    }

    private static string ReadMap(string mapName)
    {
        StreamReader reader = new StreamReader(path + mapName);
        string map = reader.ReadToEnd().Replace(Environment.NewLine, "");
        reader.Close();
        return map;
    }

    public static string[,] StringTo2DArray(string input)
    { 
        var n = mapSizeX;
        var split = input
            .Select((c, i) => new { letter = c, group = i / n })
            .GroupBy(l => l.group, l => l.letter)
            .Select(g => string.Join("", g))
            .ToArray();

        var result = new string[mapSizeX,mapSizeY];
        for (var yIndex = 0; yIndex < mapSizeY; yIndex++)
        {
            for (var xIndex = 0; xIndex < mapSizeX; xIndex++)
            {
                result[xIndex, yIndex] = split[yIndex][xIndex].ToString();
            }
        }
        
        return result;
    }
}
