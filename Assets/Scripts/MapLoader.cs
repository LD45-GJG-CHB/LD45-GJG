using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MapLoader : Singleton<MapLoader>
{
    public List<string> mapNames;
    public Dictionary<string, string> maps;
    public string TutorialMapName = "map_tutorial.txt";

    public string GetMap(string mapName)
    {
        return maps[mapName];
    }

    public string[,] GetMapAs2DArray(string mapName)
    {
        var map = AddPadding(maps[mapName].Trim().Split(new[] {Environment.NewLine}, StringSplitOptions.None));
        return StringArrayTo2DArray(map);
    }

    private void Awake()
    {
        LoadMapsFromAssets();
    }

    private void LoadMapsFromAssets()
    {
        var path = GetPath();
        mapNames = GetOrderedMapNamesFromPath(path);
        maps = new Dictionary<string, string>();
        foreach (var mapName in mapNames)
        {
            Debug.Log(mapName);
            maps.Add(mapName, LoadMap(path + mapName));
        }
    }

    private string GetPath()
    {
        return Application.streamingAssetsPath + (GameState.isBonusMaps ? "/BonusMaps/" : "/Maps/");
    }

    private List<string> GetOrderedMapNamesFromPath(string path)
    {
        return new DirectoryInfo(path)
            .GetFiles("map_*.txt").ToList()
            .OrderBy(file => GetMapIndex(file.Name.Substring(4)))
            .Select(file => file.Name).ToList();
    }

    private int GetMapIndex(string mapName)
    {
        try
        {
            return "tutorial.txt" == mapName ? int.MinValue : int.Parse(mapName.Replace(".txt", ""));
        }
        catch (FormatException)
        {
            return int.MaxValue;
        }
    }

    private string LoadMap(string mapName)
    {
        return File.ReadAllText(mapName);
    }

    private string[] AddPadding(string[] map)
    {
        int maxSizedColumn = map.OrderByDescending(s => s.Length).First().Length;
        for (var i = 0; i < map.Length; i++)
        {
            if (map[i].Length < maxSizedColumn)
            {
                map[i] = map[i] + new string('-', maxSizedColumn - map[i].Length);
            }
        }
        return map;
    }

    public string[,] StringArrayTo2DArray(string[] input)
    {
        int sizeX = input[0].Length;
        int sizeY = input.Length;

        var result = new string[sizeX, sizeY];
        for (var yIndex = 0; yIndex < sizeY; yIndex++)
        {
            for (var xIndex = 0; xIndex < sizeX; xIndex++)
            {
                result[xIndex, yIndex] = input[yIndex][xIndex].ToString();
            }
        }

        return result;
    }

}
