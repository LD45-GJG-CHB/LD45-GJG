using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps
{
    public static Dictionary<string, string> maps;

    public static int mapSizeX = 40;
    public static int mapSizeY = 20;

    static Maps()
    {
        maps = new Dictionary<string, string>();
        maps.Add("map_0", Map_0());
    }

    private static string Map_0()
    {
        return 
                
            "aaaaa" +
            "aaabb" +
            "aabba"

        ;
    }

    public static List<List<string>> convertStringMapToNestedList(string map)
    {
        List<List<string>> result = new List<List<string>>();
        for (int i = 0; i < mapSizeY; i+=mapSizeY)
        {
            List<string> row = new List<string>();
            for (int j = 0; j < mapSizeX; j++)
            {
                row.Add(map[j].ToString());
            }
            Debug.Log("row:");
            Debug.Log(row);
            result.Add(row);
        }
        Debug.Log("result");
        Debug.Log(result);
        return result;
    }
}
