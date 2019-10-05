using System.Collections;
using System.Collections.Generic;
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
  

    public static List<List<string>> convertStringMapToNestedList(string map)
    {
        map = map.Replace("\\s", "");
        Debug.Log(map.Length);
        List<List<string>> result = new List<List<string>>();
        int curX = mapSizeX;
        int curY = 0;
        for (int i = 0; i < mapSizeY - 1; i++)
        {
            result.Add(stringToList(map.Substring(curY, curX)));
            curY = curX;
            curX += mapSizeX;
        }
        Debug.Log(JsonUtility.ToJson(map));
        return result;
    }

    private static List<string> stringToList(string input)
    {
        List<string> result = new List<string>();
        foreach (char i in input)
        {
            result.Add(i.ToString());
        }
        Debug.Log(JsonUtility.ToJson(result));
        return result;
    }
}
