using System.Collections;
using System.Collections.Generic;
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
  
    public static List<List<string>> convertStringMapToNestedList(string map)
    {
        List<List<string>> result = new List<List<string>>();

        int pos = 0;
        for (int i = 0; i < mapSizeY - 1; i++)
        {
            result.Add(stringToList(map.Substring(pos, mapSizeX)));
            pos += mapSizeX;
        }
        return result;
    }

    private static List<string> stringToList(string input)
    {
        List<string> result = new List<string>();
        foreach (char i in input)
        {
            result.Add(i.ToString());
        }
        return result;
    }
}
